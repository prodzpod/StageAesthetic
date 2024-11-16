using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public static class Common
    {
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["Edge Clouds"], mr => mr.gameObject.SetActive(false)),
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(236, 161, 182, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(236, 161, 182, 255);
                }),
                new(["SMVineBody"], mr => mr.sharedMaterial.color = new Color32(144, 158, 70, 255)),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(130, 150, 171, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                }),
                new(["SGMushroom"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
            ]);
        }
        public static void ReplaceMaterials(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material detailMat3, Material detailMat4, Material detailMat5)
        {
            MaulingRockOverride = terrainMat2;
            var r = GameObject.Find("HOLDER: Randomization").transform;
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0); //Final Zone
            Assets.MeshReplaceAll([
                new(CheckParents([["Plateau", "HOLDER: Terrain"], ["SM_Rock", "FORMATION"]]), mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(CheckParents([["SMRock", "HOLDER: Spinning Rocks"], ["SMRock", "P13"], ["SM_Pebble", "Underground"], ["Boulder", "PortalDialerEvent"], ["SM_Rock", "GROUP: Rocks"], ["BbRuinPillar"]]), mr => Assets.TryMeshReplace(mr, detailMat)),
                new(CheckParents([["Terrain", "HOLDER: Terrain"], ["Plateau Under", "Underground"]]), mr => Assets.TryMeshReplace(mr, terrainMat2)),
                new(CheckParents([["Base", "PowerCoil"], ["InteractableMesh", "PortalDialerButton"]]), mr => Assets.TryMeshReplace(mr, detailMat4)),
                new(CheckParents([["Coil", "PowerCoil"]]), mr => Assets.TryMeshReplace(mr, detailMat5)),
                new(["SM_Pebble", "mdlGeyser"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["SMSpikeBridge"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["PowerLine", "MegaTeleporter", "SMRuinGateDoor", "SMRuinArch"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(["HumanCrate", "SMRuinPillar"], mr => Assets.TryMeshReplace(mr, detailMat4))
            ]);
            btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2; //Terrain
            GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            GameObject.Find("SM_Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            J[] js = [
                // Plateau Tall
                new(r, [0, 0, 0], terrainMat2),
                new(r, [0, 0, 1], detailMat2),
                new(r, [0, 1, 0], terrainMat2),
                new(r, [0, 1, 1], detailMat2),
                // Plateau 6
                new(r, "GROUP: Plateau 6/P6 Option 2/SM_Plateau 6 Option 2", [], terrainMat2), //Plateau 6 Option 2 (1)
                new(r, "GROUP: Plateau 6/P6 Option 2", [1], detailMat2), //SMSpikeBridge (6)
                new(r, "GROUP: Plateau 6/P6 Option 2", [2], detailMat2), //SMSpikeBridge (7)
                new(r, "GROUP: Plateau 6/P6 Option 3", [0], terrainMat2), //Plateau 6 Option 3 (1)
                new(r, "GROUP: Plateau 6/P6 Option 3", [11], detailMat2), //SMSpikeBridge (8)
                new(r, "GROUP: Plateau 6/P6 Option 3", [13], detailMat2), //SMSpikeBridge (9)
                // Plateau 9
                new(r, [2, 0, 0], terrainMat2), //Plateau 9 Option 1 (1)
                new(r, [2, 0, 1], detailMat2), //SMSpikeBridge (16)
                new(r, [2, 0, 2], detailMat2), //SMSpikeBridge (15)
                new(r, [2, 1, 0], detailMat2), //SMSpikeBridge (17)
                // Plateau 11
                new(r, [3, 0, 0], terrainMat2), //Plateau 11 Bridge (1)
                // Plateau 13
                new(r, [4, 0, 1], terrainMat), //SM_Plateau 13
                new(r, [4, 1, 0], terrainMat2), //Rock Connect (1)
                // Plateau 15
                new(r, [5, 0, 0], terrainMat2), //Plateau 15 (1)
                new(r, [5, 0, 10], detailMat2), //SMSpikeBridge (14)
                new(r, [5, 0, 11], detailMat2), //SMSpikeBridge
                new(r, [5, 1, 0], terrainMat2), //Plateau 15 Option 1 (1)
                new(r, [5, 1, 1], detailMat2), //SMSpikeBridge
                new(r, [5, 1, 2], detailMat2), //SMSpikeBridge (13)
                new(r, [5, 2, 0], detailMat2), //SMSpikeBridge (3)
                // btp
                new(btp, [4], x => x.gameObject.SetActive(false)), //MiscProps
                new(btp, [5], x => x.gameObject.SetActive(false)), //LShapeScaffolding
                new(btp, [6], x => x.gameObject.SetActive(false)), //StaircaseScaffolding
                new(btp, [3, 1], x => x.gameObject.SetActive(false)), //ChainlinkSet
                new(btp, [3, 2], x => x.gameObject.SetActive(false)), //ChainlinkSet (1)
                new(btp, [3, 3], x => x.gameObject.SetActive(false)), //ChainlinkSet (2)
                new(btp, [3, 4], x => x.gameObject.SetActive(false)), //ChainlinkSet (3)
                new(btp, [3, 5], x => x.gameObject.SetActive(false)), //MiscProps
                new(btp, [2, 9], x => x.gameObject.SetActive(false)), //Barriers
            ];
            foreach (var j in js)
            {
                var c = j.root;
                if (!string.IsNullOrWhiteSpace(j.find)) c = c.Find(j.find);
                j.ret(c.GetDescendant(j.hierarchy));
            }
            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);
        }
        public static Func<MeshRenderer, bool> CheckParents(string[][] args)
        {
            return mr => args.Any(x =>
            {
                if (!mr.gameObject.name.Contains(x[0])) return false;
                if (x.Length == 1) return true;
                var parent = mr.transform.parent;
                if (!parent) return false;
                return parent.gameObject.name.Contains(x[1]);
            });
        }
        public class J(Transform root, string find, int[] hierarchy, Action<Transform> ret)
        {
            public Transform root = root;
            public string find = find;
            public int[] hierarchy = hierarchy;
            public Action<Transform> ret = ret;
            public J(Transform root, int[] hierarchy, Action<Transform> ret) : this(root, "", hierarchy, ret) { }
            public J(Transform root, string find, int[] hierarchy, Material ret) : this(root, find, hierarchy, m => { var mr = m.GetComponent<MeshRenderer>(); if (mr) mr.sharedMaterial = ret; }) { }
            public J(Transform root, int[] hierarchy, Material ret) : this(root, "", hierarchy, ret) { }
        }

        public static Material MaulingRockOverride = null;
        public static Dictionary<GameObject, GameObject> Rocks = [];
        public static void AddHook()
        {
            IL.MaulingRockZoneManager.FireRock += il =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(out _));
                c.EmitDelegate<Func<GameObject, GameObject>>(rock =>
                {
                    if (MaulingRockOverride == null || !rock || !rock.transform.Find("Model")) return rock;
                    GameObject ret;
                    if (Rocks.ContainsKey(rock)) ret = Rocks[rock];
                    else { ret = UnityEngine.Object.Instantiate(rock); Rocks[rock] = ret; }
                    var mr = ret.transform.Find("Model").GetComponent<MeshRenderer>();
                    Assets.TryMeshReplace(mr, MaulingRockOverride);
                    return ret;
                });
            };
        }
    }
}
