using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.VerdantFalls
{
    public class Sunny : Variant
    {
        public override string[] Stages => ["lakes"];
        public override string Name => nameof(Sunny);
        public override string Description => "Sunny and bright.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.DaySky();
            GameObject.Find("Directional Light (SUN)").GetComponent<Light>().color = new Color(0.9333f, 0.8275f, 0.3361f, 1);
        }
    }
}
