using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage4.SunderedGrove
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["rootjungle"];
        public override string Name => nameof(Overcast);
        public override string Description => "Rainy with extra fog.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(203, 221, 243, 255);
            sunLight.intensity = 3f;

            fog.fogColorStart.value = new Color32(44, 45, 58, 17);
            fog.fogColorMid.value = new Color32(46, 50, 60, 132);
            fog.fogColorEnd.value = new Color32(76, 81, 84, 255);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.095f;
            fog.skyboxStrength.value = 0.126f;
            cgrade.colorFilter.value = new Color32(148, 206, 183, 255);
            cgrade.colorFilter.overrideState = true;

            // cgrade.colorFilter.value = new Color32(148, 206, 183, 255);
            //cgrade.colorFilter.overrideState = true;

            Common.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            GameObject.Find("HOLDER: Weather Set 1").transform.Find("CameraRelative").gameObject.SetActive(false);
            Weather.AddRain(Intensity.Extreme);
        }
    }   
}
