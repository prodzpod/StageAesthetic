using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.ShatteredAbodes
{
    public class Verdant : Variant
    {
        public override string[] Stages => ["village"];
        public override string Name => nameof(Verdant);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;
            fog.fogIntensity.value = 0.4f;
            fog.fogPower.value = 0.5f;
            fog.skyboxStrength.value = 0f;

            GameObject rainParticles = GameObject.Find("CAMERA PARTICLES: RainParticles (1)");
            rainParticles.SetActive(false);
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            sun.transform.eulerAngles = new Vector3(60f, 65f, 210f);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.7f;
            // 30.512 64.27 209.701
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            var verdantTerrainMat = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLTerrainStone.mat"));
            verdantTerrainMat.SetFloat("_RedChannelBias", 0.4f);
            verdantTerrainMat.SetFloat("_GreenChannelBias", -0.2f);
            verdantTerrainMat.SetFloat("_BlueChannelBias", -0.03f);
            AbodesMaterials(verdantTerrainMat,
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLRocks.mat"),
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLShip.mat"),
                Assets.LoadRecolor("RoR2/DLC2/lakes/Assets/matTLGVine.mat", new Color32(255, 255, 255, 255))
            );
        }

        public static void AbodesMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            Assets.MeshReplaceAll([
                new(["Grass", "Fern"], mr => Object.Destroy(mr.gameObject)),
                new(["HouseBuried", "LVTerrain", "LVArc_StormOutlook", "BuriedHouse"], mr => { if (mr.sharedMaterials.Length == 2) mr.sharedMaterials = [terrainMat, detailMat2]; }),
                new(["LVTerrainToggle", "LVTerrainFar", "Dune", "BrokenAltar", "LVTerrainBackground"], mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(["LVArc_Temple", "LVArc_Houses", "LVArc_CliffCave", "LVArc_Bridge", "LVArc_BrokenPillar"], mr => { if (mr.sharedMaterials.Length == 2) mr.sharedMaterials = [detailMat2, terrainMat]; }),
                new(["Pillar"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["RockMedium", "Pebble"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["Crystal"], mr => Assets.TryMeshReplace(mr, detailMat3)),
            ]);
        }
    }
}
