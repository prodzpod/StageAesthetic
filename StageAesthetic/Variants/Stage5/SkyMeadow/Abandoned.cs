using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TerrainUtils;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Abandoned : Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Abandoned);
        public override string Description => "Texture swap to Yellow Abandoned Aqueduct.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop); 
            fog.fogColorStart.value = new Color32(113, 42, 109, 97);
            fog.fogColorMid.value = new Color32(174, 135, 66, 60);
            fog.fogColorEnd.value = new Color32(128, 101, 59, 255);
            fog.fogZero.value = -0.05f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.7f;
            fog.fogOne.value = 0.25f;
            fog.skyboxStrength.value = 0f;
            Common.ReplaceMaterials(
                Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(230, 223, 174, 219)),
                Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat", new Color32(255, 188, 160, 223)),
                Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat"),
                Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat"),
                Assets.LoadRecolor("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionWild.mat", new Color32(248, 219, 175, 255)),
                Assets.LoadRecolor("RoR2/Base/Common/TrimSheets/matTrimSheetSwampyRuinsProjectedLight.mat", new Color32(217, 191, 168, 255)),
                Assets.Load<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat")
            );
            var c = GameObject.Find("Cloud Floor").transform;
            c.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/Base/Common/matClayGooDebuff.mat"); //Cloud1
            GameObject.Find("Hard Floor").SetActive(false);
            c.GetChild(0).localPosition = new Vector3(0, 30, 0);
            c.GetChild(0).localScale = new Vector3(600, 600, 600);
            c.GetChild(1).gameObject.SetActive(false); //Cloud2
            c.GetChild(2).gameObject.SetActive(false); //Cloud3
            c.GetChild(3).gameObject.SetActive(false); //Cloud4
            Assets.MeshReplaceAll([
                new(["BbRuinPillar"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetClayPots.mat"))),
                new(["BbRuinArch"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(230, 223, 174, 219)))),
            ]);
            SandyFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Extreme);
        public static void SandyFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(255, 186, 95, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(216, 171, 88, 255);
                }),
                new(["SMVineBody", "spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(213, 158, 70, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(73, 58, 42, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(84, 68, 49, 255);
                }),
                new(["SGMushroom"], mr => mr.sharedMaterial.color = new Color32(255, 90, 0, 255)),
            ]);
        }
    }   
}
