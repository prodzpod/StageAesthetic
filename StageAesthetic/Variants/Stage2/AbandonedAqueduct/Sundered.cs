using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class Sundered : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => nameof(Sundered);
        public override string Description => "Texture swap to Pink Sundered Grove.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(83, 53, 95, 35);
            fog.fogColorMid.value = new Color32(91, 61, 117, 75);
            fog.fogColorEnd.value = new Color32(107, 66, 111, 255);
            fog.skyboxStrength.value = 0.055f;
            fog.fogOne.value = 0.082f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = UnityEngine.Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
            newSun.intensity = 1.6f;
            newSun.color = new Color32(134, 193, 216, 255);
            var waterfall = GameObject.Find("HOLDER: GameplaySpace").transform.Find("mdlGlDam/GL_AqueductPartial/GL_Waterfall").transform;
            waterfall.gameObject.SetActive(true);
            waterfall.GetComponent<MeshRenderer>().enabled = false;
            waterfall.Find("FoamOverParticles").gameObject.SetActive(false);
            waterfall.Find("DEBUFF ZONE: Waterfall/PP Goo").gameObject.SetActive(false);
            GameObject.Find("GLUndergroundPPVolume").SetActive(false);
            var caveLight = GameObject.Find("AmbientLight").GetComponent<Light>();
            caveLight.color = new Color32(150, 29, 119, 255);
            cgrade.saturation.value = -2f;
            SunderedFoliage();
            SunderedMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Mild);
        public static void SunderedFoliage()
            => Assets.ReplaceAll<LineRenderer>([new(_ => true, lr => lr.sharedMaterial.color = new Color32(115, 57, 75, 255))]);
        public static void SunderedMaterials()
        {
            Assets.MeshReplaceAll([
                new(["Terrain"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/rootjungle/matRJTerrain2.mat", new Color32(255, 156, 206, 184)))), // aqueductSunderedTerrainMat
                new(["SandDune"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/rootjungle/matRJTerrain.mat"))), // aqueductSunderedTerrainMat2
                new(["SandstonePillar", "Dam", "AqueductFullLong", "AqueductPartial"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/rootjungle/matRJSandstone.mat"))), // aqueductSunderedDetailMat2
                new(["RuinedRing", "Boulder", "Eel"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/rootjungle/matRJTerrain2.mat", new Color32(221, 77, 102, 231)))), // aqueductSunderedDetailMat
                new(mr => mr.gameObject.name.Contains("Bridge") && !mr.gameObject.name.Contains("Decal"), mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/rootjungle/matRJSandstone.mat", new Color32(221, 77, 102, 231)))), // aqueductSunderedDetailMat
                new(["FlagPoleMesh", "RuinTile"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/rootjungle/matRJTree.mat"))), // aqueductSunderedDetailMat3
                new(["AqueductCap"], mr => Assets.MeshReplaceAll(mr, Assets.LoadRecolor("RoR2/DLC1/voidstage/matVoidMetalTrimGrassy.mat", new Color32(130, 61, 74, 150)))) // aqueductSunderedDetailMat2
            ]);
        }
    }
}
