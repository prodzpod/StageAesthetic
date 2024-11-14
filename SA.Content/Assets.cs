using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using HG;
using static StageAesthetic.Assets;

namespace StageAesthetic
{
    public class Assets
    {
        public static Shader CloudRemap;
        public static Shader SnowTopped;
        public static Dictionary<KeyValuePair<string, Type>, object> Cache = [];
        public static Dictionary<KeyValuePair<string, Color32>, Material> CacheRecolor = [];
        public static T Load<T>(string path)
        {
            KeyValuePair<string, Type> k = new(path, typeof(T));
            if (Cache.ContainsKey(k)) return (T)Cache[k];
            var ret = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
            Cache[k] = ret; return ret;
        }
        public static Material LoadRecolor(string path, Color32 color)
        {
            KeyValuePair<string, Color32> k = new(path, color);
            if (CacheRecolor.ContainsKey(k)) return CacheRecolor[k];
            var g = UnityEngine.Object.Instantiate(Load<Material>(path));
            g.color = color; CacheRecolor[k] = g; return g;
        }
        public static void TryDestroy(string path) { GameObject a = GameObject.Find(path); if (a) UnityEngine.Object.Destroy(a); }
        public static void TryDestroy(Transform parent, string path) { GameObject a = parent.Find(path).gameObject; if (a) UnityEngine.Object.Destroy(a); }
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
                        if (CacheMaterial.ContainsKey(k)) v = CacheMaterial[k];
                        else v = UnityEngine.Object.Instantiate(k);
                        mr.sharedMaterials[i] = v;
                    }
                    action.Action(mr);
                }
            }
        }
        public static void TryMeshReplace(MeshRenderer mr, Material material) { if (mr?.sharedMaterial) mr.sharedMaterial = material; }
        public static void LightReplaceAll(params ReplaceInstance<Light>[] actions)
        {
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light l in lightList)
            {
                if (!l.gameObject) continue;
                foreach (var action in actions) if (action.Condition(l)) action.Action(l);
            }
        }
        public static void ParticleReplaceAll(params ReplaceInstance<ParticleSystem>[] actions)
        {
            var particleList = UnityEngine.Object.FindObjectsOfType(typeof(ParticleSystem)) as ParticleSystem[];
            foreach (ParticleSystem ps in particleList)
            {
                if (!ps.gameObject) continue;
                foreach (var action in actions) if (action.Condition(ps)) action.Action(ps);
            }
        }
        public class ReplaceInstance<T>(Func<T, bool> condition, Action<T> action) where T : Component
        {
            public Func<T, bool> Condition = condition;
            public Action<T> Action = action;
            public ReplaceInstance(string[] keys, Action<T> action) : this(x => keys.Any(x.gameObject.name.Contains), action) { }
        }
    }
}
