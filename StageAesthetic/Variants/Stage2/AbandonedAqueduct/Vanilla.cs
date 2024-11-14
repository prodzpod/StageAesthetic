using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.4f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Mild);
        public static void VanillaFoliage()
            => Assets.ReplaceAll<LineRenderer>([new(_ => true, lr => lr.sharedMaterial.color = new Color32(141, 42, 42, 255))]);
    }
}
