using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Nostalgic : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Nostalgic);
        public override string Description => "Early Access look.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            volume.profile = Assets.Load<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGolemplains.asset");
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(0.7450981f, 0.8999812f, 0.9137255f);
            sun.intensity = 1.34f;

            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Common.VanillaFoliage();
        }
    }
}
