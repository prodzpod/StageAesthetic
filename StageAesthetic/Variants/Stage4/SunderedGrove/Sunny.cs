using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.SunderedGrove
{
    public class Sunny : Variant
    {
        public override string[] Stages => ["rootjungle"];
        public override string Name => nameof(Sunny);
        public override string Description => "Yellow sun over greenery.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(128, 121, 99, 13);
            fog.fogColorMid.value = new Color32(106, 141, 154, 60);
            fog.fogColorEnd.value = new Color32(104, 150, 199, 150);
            fog.fogZero.value = -0.058f;
            fog.fogPower.value = 1.2f;
            fog.fogIntensity.value = 0.937f;
            fog.skyboxStrength.value = 0.26f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 225, 181, 255);
            sunLight.intensity = 1.8f;
            sunTransform.localEulerAngles = new Vector3(60, 15, -4);
            Common.VanillaFoliage();
        }
    }   
}
