using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(239, 231, 211, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 1f;
            Common.VanillaFoliage();
        }
    }   
}
