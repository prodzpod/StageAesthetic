using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["blackbeach", "blackbeach2"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Mild);
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["bbSimpleGrassPrefab"], mr => mr.sharedMaterial.color = new Color32(11, 58, 28, 255)),
                new(["spmBbFern2"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
                new(["spmBbFern3"], mr => mr.sharedMaterial.color = new Color32(229, 229, 229, 255)),
                new(["spmBush"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(255, 255, 255, 255);
                }),
                new(["spmBbDryBush"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(125, 125, 128, 255);
                }),
                new(["Ivy"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(40, 47, 30, 146);
                }),
                new(["Vine"], mr => mr.sharedMaterial.color = new Color32(44, 49, 27, 255)),
                new(["spmBbConif_", "spmBbConifYoung"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(125, 125, 128, 255);
                }),
            ]);
        }
    }
}
