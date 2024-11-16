using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TerrainUtils;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Titanic : Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Titanic);
        public override string Description => "Texture swap to Titanic Plains.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(125, 141, 160, 0);
            fog.fogColorMid.value = new Color32(119, 144, 175, 60);
            fog.fogColorEnd.value = new Color32(94, 137, 195, 110);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;
            Common.ReplaceMaterials(
                Assets.Load<Material>("RoR2/Base/golemplains/matGPTerrain.mat"),
                Assets.Load<Material>("RoR2/Base/golemplains/matGPTerrainBlender.mat"),
                Assets.Load<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat"),
                Assets.Load<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat"),
                Assets.Load<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardProps.mat"),
                Assets.Load<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat"),
                Assets.Load<Material>("RoR2/Junk/AncientWisp/matAncientWillowispSpiral.mat")
            );
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/Base/golemplains/matGPTerrain.mat");
            TitanicFoliage();
        }

        public static void TitanicFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(198, 255, 95, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(213, 246, 99, 255);
                }),
                new(["SMVineBody", "spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(144, 158, 70, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(133, 191, 127, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(192, 255, 0, 255);
                }),
                new(["SGMushroom"], mr => mr.sharedMaterial.color = new Color32(162, 176, 46, 255)),
            ]);
        }
    }   
}
