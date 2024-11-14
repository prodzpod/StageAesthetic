using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using HG;

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
        public static Dictionary<Material, Material> CacheMaterial = [];
        public static void MeshReplaceAll(params MeshReplaceInstance[] actions)
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
        public class MeshReplaceInstance(Func<MeshRenderer, bool> condition, Action<MeshRenderer> action)
        {
            public Func<MeshRenderer, bool> Condition = condition;
            public Action<MeshRenderer> Action = action;
            public MeshReplaceInstance(string[] keys, Action<MeshRenderer> action): this(mr => keys.Any(mr.gameObject.name.Contains), action) { }
        }
        public static void TryMeshReplace(MeshRenderer mr, Material material) { if (mr?.sharedMaterial) mr.sharedMaterial = material; }
    }
}
