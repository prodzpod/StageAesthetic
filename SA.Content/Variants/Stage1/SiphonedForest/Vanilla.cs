using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.SiphonedForest
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["snowyforest"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        { DisableSiphonedSnow(); Weather.AddSnow(Intensity.Medium); }
        public static void DisableSiphonedSnow()
            => GameObject.Find("HOLDER: Skybox").transform.Find("CAMERA PARTICLES: SnowParticles").gameObject.SetActive(false);
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmGPGrass_LOD0"], mr => mr.sharedMaterial.color = new Color32(168, 168, 141, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(125, 125, 128, 255);
                })
            ]);
        }
    }
}
