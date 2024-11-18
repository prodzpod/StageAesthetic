using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;
using Newtonsoft.Json;
using BepInEx.Configuration;
using System.Reflection;
using StageAesthetic.Variants;
using IL.RoR2.UI.LogBook;
using HarmonyLib;
using MonoMod.Cil;
using R2API;
using Mono.Cecil.Cil;
using UnityEngine.SceneManagement;

namespace StageAesthetic
{
    public class Assets
    {
        public static ConfigEntry<string> VariantsLoaded;
        public static ConfigEntry<string> ResourcesLoaded;
        public static Dictionary<string, List<string>> PreloadedVariants = [];
        public static Dictionary<KeyValuePair<string, Type>, object> Cache = [];
        public static Dictionary<KeyValuePair<string, Color32>, Material> CacheRecolor = [];
        public static int _temp = 0;
        public static void Init()
        {
            VariantsLoaded = ConfigManager.BackupConfig.Bind("General", "Variants Loaded", "{}", "do not change this");
            ResourcesLoaded = ConfigManager.BackupConfig.Bind("General", "Resources To Load", "{}", "do not change this");
            // preload first
            PreloadedVariants = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(VariantsLoaded.Value);
            Dictionary<Type, List<string>> ResourcesToLoad = [];
            var _r = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(ResourcesLoaded.Value);
            foreach (var ts in _r.Keys)
            {
                Type t = Type.GetType(ts);
                MethodInfo m = typeof(Assets).GetMethod(nameof(Load), BindingFlags.Public | BindingFlags.Static);
                m = m.MakeGenericMethod(t);
                foreach (var g in _r[ts]) m.Invoke(null, [g]);
            }
            // check for missing ones
            Dictionary<string, List<string>> VariantsToLoad = [];
            int cnt = 0;
            foreach (var v in Variant.Variants)
            {
                foreach (var s in v.Stages)
                {
                    if (PreloadedVariants.ContainsKey(s) && PreloadedVariants[s].Contains(v.Name)) continue;
                    if (!VariantsToLoad.ContainsKey(s)) VariantsToLoad[s] = [];
                    VariantsToLoad[s].Add(v.Name);
                    cnt++;
                }
            }
            Main.Log.LogInfo($"[initial load :3] Preloading {cnt} variants across {VariantsToLoad.Count} stages");
            _temp = VariantsToLoad.Count;
            Main.Harmony.PatchAll(typeof(RemoveLogsDuringPreload));
            foreach (var s in VariantsToLoad.Keys)
                SceneAssetAPI.AddAssetRequest(s, gos => {
                    DisableLoad = true;
                    foreach (var v in VariantsToLoad[s])
                    {
                        try { Hooks.RollVariantInternal(s, v); }
                        catch { Main.Log.LogWarning("Failed to load this variant fully, some ourple may occur the first time you load this variant"); }
                        if (!PreloadedVariants.ContainsKey(s)) PreloadedVariants[s] = [];
                        PreloadedVariants[s].Add(v);
                    }
                    Tick();
                });
        }
        public static void Tick()
        {
            _temp--;
            if (_temp == 0)
            {
                Main.Log.LogInfo("Done Preloading");
                PostProcessVolume.localVolumes = [];
                SaveResourceList();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        public static void SaveResourceList()
        {
            Main.Log.LogInfo("Saving Resource List");
            DisableLoad = false;
            Dictionary<string, List<string>> g = [];
            foreach (var e in Cache.Keys)
            {
                if (!g.ContainsKey(e.Value.AssemblyQualifiedName)) g[e.Value.AssemblyQualifiedName] = [];
                g[e.Value.AssemblyQualifiedName].Add(e.Key);
            }
            ResourcesLoaded.Value = JsonConvert.SerializeObject(g);
            VariantsLoaded.Value = JsonConvert.SerializeObject(PreloadedVariants);
        }
        public static Shader CloudRemap;
        public static Shader SnowTopped;
        public static PostProcessProfile GooLakeProfile;
        public static bool DisableLoad = false;
        public static T Load<T>(string path)
        {
            KeyValuePair<string, Type> k = new(path, typeof(T));
            if (Cache.ContainsKey(k) && Cache[k] != default) return (T)Cache[k];
            T ret = DisableLoad ? default : Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
            if (!string.IsNullOrWhiteSpace(Hooks.currentVariantName) && !Cache.ContainsKey(k)) 
            { 
                Main.Log.LogWarning("Un-preloaded asset detected, fixing");
                SaveResourceList();
            }
            Cache[k] = ret;
            if (ret == null)
            {
                if (ret is Material) return (T)(object)new Material(new Shader());
                if (ret is Texture2D) return (T)(object)new Texture2D(1, 1);
                if (ret is GameObject) return (T)(object)new GameObject();
            }
            return ret;
        }
        public static Material LoadRecolor(string path, Color32 color)
        {
            KeyValuePair<string, Color32> k = new(path, color);
            if (CacheRecolor.ContainsKey(k)) return CacheRecolor[k];
            var m = Load<Material>(path); if (!m) return m;
            var g = UnityEngine.Object.Instantiate(m);
            g.color = color; if (Cache[new(path, typeof(Material))] != default) CacheRecolor[k] = g; return g;
        }
        public static void TryDestroy(string path) { GameObject a = GameObject.Find(path); if (a) UnityEngine.Object.Destroy(a); }
        public static void TryDestroy(Transform parent, string path) { Transform a = parent.Find(path); if (a) UnityEngine.Object.Destroy(a.gameObject); }
        public static Dictionary<Material, Material> CacheMaterial = [];
        public static void MeshReplaceAll(params ReplaceInstance<MeshRenderer>[] actions)
        {
            var meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                if (!mr.gameObject) continue;
                foreach (var action in actions) if (action.Condition(mr))
                {
                    for (var i = 0; i < mr.sharedMaterials.Length; i++)
                    {
                        Material k = mr.sharedMaterials[i], v = null;
                        if (!k) k = new(new Shader());
                        if (CacheMaterial.ContainsKey(k)) v = CacheMaterial[k];
                        else { v = UnityEngine.Object.Instantiate(k); CacheMaterial[k] = v; }
                        mr.sharedMaterials[i] = v;
                    }
                    action.Action(mr);
                }
            }
        }
        public static void MeshReplaceAll(MeshRenderer mr, Material material)
        {
            var m = mr.sharedMaterials;
            for (var i = 0; i < m.Length; i++) m[i] = material;
            mr.sharedMaterials = m;
        }
        public static void TryMeshReplace(MeshRenderer mr, Material material) { if (mr?.sharedMaterial) mr.sharedMaterial = material; }
        public static void ReplaceAll<T>(params ReplaceInstance<T>[] actions) where T : Component
        {
            var particleList = UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
            foreach (T x in particleList)
            {
                if (!x.gameObject) continue;
                foreach (var action in actions) if (action.Condition(x)) action.Action(x);
            }
        }
        public class ReplaceInstance<T>(Func<T, bool> condition, Action<T> action) where T : Component
        {
            public Func<T, bool> Condition = condition;
            public Action<T> Action = action;
            public ReplaceInstance(string[] keys, Action<T> action) : this(x => keys.Any(x.gameObject.name.Contains), action) { }
        }
    }
    [HarmonyPatch(typeof(SceneAssetAPI), nameof(SceneAssetAPI.PrepareRequests))]
    public class RemoveLogsDuringPreload
    {
        public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
        {
            ILCursor c = new(il);
            c.GotoNext(x => x.MatchLdstr(" doesnt exist, available scene names:"));
            c.GotoNext(x => x.MatchLeaveS(out _));
            var l = c.MarkLabel();
            c.GotoPrev(x => x.MatchLdstr(" doesnt exist, available scene names:"));
            c.GotoPrev(MoveType.AfterLabel, x => x.MatchCallOrCallvirt<SceneAssetPlugin>("get_" + nameof(SceneAssetPlugin.Logger)));
            c.EmitDelegate(() =>
            {
                if (Assets._temp == 0) return false;
                Assets.Tick();
                return true;
            });
            c.Emit(OpCodes.Brtrue, l);
        }
    }
}
