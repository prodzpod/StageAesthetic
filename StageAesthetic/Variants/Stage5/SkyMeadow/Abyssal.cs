using JetBrains.Annotations;
using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TerrainUtils;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Abyssal : Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Abyssal);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(79, 79, 103, 255);
            cgrade.saturation.value = -8f;
            fog.fogColorStart.value = new Color32(106, 22, 107, 50);
            fog.fogColorMid.value = new Color32(39, 85, 97, 44);
            fog.fogColorEnd.value = new Color32(35, 76, 73, 255);
            fog.fogZero.value = -0.1f;
            fog.fogOne.value = 0.2f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.6f;
            fog.skyboxStrength.value = 0f;

            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var hardFloor = lightBase.GetChild(6);
            hardFloor.gameObject.SetActive(false);
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).gameObject.SetActive(false);
            var sun = lightBase.GetChild(0).GetComponent<Light>();
            sun.color = new Color32(118, 222, 225, 255);
            sun.intensity = 2f;
            sun.shadowStrength = 0.75f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            lightBase.GetChild(6).localScale = new Vector3(4000f, 4000f, 4000f);
            lightBase.GetChild(6).GetComponent<MeshRenderer>().sharedMaterial =
                Assets.LoadRecolor("RoR2/Base/Cleanse/matWaterPack.mat", new Color32(217, 0, 255, 255));
            // lightbase
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            ReplaceMaterials(
                Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainFloor.mat", new Color32(255, 0, 0, 255)),
                Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainFloor.mat", new Color32(255, 0, 0, 255)),
                Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"),
                Assets.Load<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat"),
                Assets.LoadRecolor("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionDestroyed.mat", new Color32(255, 136, 103, 255)),
                Assets.Load<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat"),
                Assets.LoadRecolor("RoR2/Base/dampcave/matDCCoralActive.mat", new Color32(255, 10, 0, 255))
            );
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainFloor.mat", new Color32(255, 0, 0, 255));
            AbyssalFoliage();
        }
        public static void ReplaceMaterials(Material terrainMat, Material terrainMat2, Material detailMat, Material detailMat2, Material detailMat3, Material detailMat4, Material detailMat5)
        {

            var r = GameObject.Find("HOLDER: Randomization").transform;
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0); //Final Zone
            Assets.MeshReplaceAll([
                new(CheckParents([["Plateau", "skymeadow_terrain"], ["SMRock", "FORMATION"]]), mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(CheckParents([["SMRock", "HOLDER: Spinning Rocks"], ["SMRock", "P13"], ["SMPebble", "Underground"], ["Boulder", "PortalDialerEvent"], ["SMRock", "GROUP: Rocks"], ["BbRuinPillar"]]), mr => Assets.TryMeshReplace(mr, detailMat)),
                new(CheckParents([["SMSpikeBridge", "Underground"]]), mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(CheckParents([["Terrain", "skymeadow_terrain"], ["Plateau Under", "Underground"]]), mr => Assets.TryMeshReplace(mr, terrainMat2)),
                new(CheckParents([["Base", "PowerCoil"], ["InteractableMesh", "PortalDialerButton"]]), mr => Assets.TryMeshReplace(mr, detailMat4)),
                new(CheckParents([["Coil", "PowerCoil"]]), mr => Assets.TryMeshReplace(mr, detailMat5)),
                new(["SMPebble", "mdlGeyser"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["SMSpikeBridge"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["PowerLine", "MegaTeleporter", "BbRuinGateDoor", "BbRuinArch"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(["HumanCrate", "BbRuinPillar"], mr => Assets.TryMeshReplace(mr, detailMat4))
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
        public static void AbyssalFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(255, 165, 0, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(255, 125, 0, 255);
                }),
                new(["SMVineBody"], mr => mr.sharedMaterial.color = new Color32(166, 120, 37, 255)),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(181, 77, 45, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(191, 163, 127, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(255, 209, 0, 255);
                }),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(255, 108, 0, 255)),
            ]);
        }
        public static Func<MeshRenderer, bool> CheckParents(string[][] args)
        {
            return mr => args.Any(x =>
            {
                if (!mr.gameObject.name.Contains(x[0])) return false;
                if (x.Length == 1) return true;
                var parent = mr.transform.parent.gameObject;
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
    }   
}
