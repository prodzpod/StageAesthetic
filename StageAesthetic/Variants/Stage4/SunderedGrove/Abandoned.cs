using RiskOfOptions.Resources;
using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage4.SunderedGrove
{
    public class Abandoned : Variant
    {
        public override string[] Stages => ["rootjungle"];
        public override string Name => nameof(Abandoned);
        public override string Description => "Texture swap to Orange Abandoned Aqueduct.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop); 
            RampFog rampFog = Assets.GooLakeProfile.GetSetting<RampFog>();
            fog.fogColorStart.value = new Color(0.49f, 0.363f, 0.374f, 0f);
            fog.fogColorMid.value = new Color(0.58f, 0.486f, 0.331f, 0.25f);
            fog.fogColorEnd.value = new Color(0.77f, 0.839f, 0.482f, 0.5f);
            fog.fogZero.value = rampFog.fogZero.value;
            fog.fogIntensity.value = rampFog.fogIntensity.value;
            fog.fogPower.value = rampFog.fogPower.value;
            fog.fogOne.value = rampFog.fogOne.value;
            fog.skyboxStrength.value = 0.02f;

            var c = GameObject.Find("Cloud Floor").transform;
            c.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/Base/Common/matClayGooDebuff.mat");
            Assets.ReplaceAll<SkinnedMeshRenderer>([new(["BounceStem"], mr => { if (mr.sharedMaterial) mr.sharedMaterial = Assets.Load<Material>("RoR2/Base/Common/matClayGooDebuff.mat"); })]);
            Assets.MeshReplaceAll([
                new(["Terrain", "Gianticus", "Tree Big Bottom", "Tree D", "Wall", "RJ_Root", "RJ_ShroomShelf"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(255, 222, 185, 39)))),
                new(["RJ_Triangle", "RJ_RuinArch", "RJ_ShroomBig", "Rock", "Boulder", "Pebble", "Root Bridge", "Vine Tree"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat", new Color32(166, 157, 27, 59)))),
                new(["Moss Cover", "RJ_ShroomShelf", "RJ_ShroomBig", "RJ_ShroomSmall", "RJ_MossPatch"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolake.mat", new Color32(176, 153, 57, 255)))),
                new(["RJ_TwistedTreeBig"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(255, 222, 185, 39)))),
            ]);
            Assets.ReplaceAll<Light>([new(_ => true,l => {
                l.color = new Color32(255, 131, 117, 255);
                l.range = 15;
                l.intensity = 1f;
            })]);
            SandyFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Medium);
        public static void SandyFoliage()
        {
            Assets.MeshReplaceAll([
                new(["RJShroomFoliage_", "RJTreeBigFoliage_"], mr => mr.sharedMaterial.color = new Color32(0, 0, 0, 255)),
                new(["RJMossFoliage_"], mr => mr.sharedMaterial.color = new Color32(255, 149, 0, 255)),
                new(["RJTowerTreeFoliage_"], mr => { foreach (var a in mr.sharedMaterials) a.color = new Color32(255, 149, 0, 255); }),
                new(["RJHangingMoss_"], mr => mr.sharedMaterial.color = new Color32(0, 0, 0, 102)),
                new(["spmFern1_"], mr => mr.sharedMaterial.color = new Color32(255, 101, 58, 86)),
                new(["spmRJgrass1_"], mr => mr.sharedMaterial.color = new Color32(255, 37, 0, 255)),
                new(["spmRJgrass2_"], mr => mr.sharedMaterial.color = new Color32(0, 0, 0, 0)),
            ]);
        }
    }   
}
