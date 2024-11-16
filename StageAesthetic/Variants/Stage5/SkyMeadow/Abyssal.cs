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
        public override string Description => "Texture swap to Red Abyssal Depths.";
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
            Common.ReplaceMaterials(
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
    }   
}
