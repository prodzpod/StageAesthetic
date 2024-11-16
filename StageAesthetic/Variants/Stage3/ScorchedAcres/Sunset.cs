using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Sunset : Variant
    {
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Sunset);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(30, 16, 52, 40);
            fog.fogColorMid.value = new Color32(123, 58, 40, 48);
            fog.fogColorEnd.value = new Color32(84, 32, 3, 222);
            fog.skyboxStrength.value = 0.076f;
            fog.fogZero.value = -0.019f;
            fog.fogOne.value = 0.211f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.6f;
            cgrade.colorFilter.value = new Color(1f, 1f, 1f, 0.08f);
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 149, 128, 255);
            sunLight.intensity = 2.1f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(35f, 198.5f, 218.841f);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            Vector3 sunPosition = sunBase.parent.localPosition;
            sunPosition.y = -67;
            Transform quad = sunBase.GetChild(1);
            quad.localScale = new Vector3(14, 14, 14);
            quad.localEulerAngles = new Vector3(270, 30, 0);
            quad.localPosition = new Vector3(0, 0, 0);
            sunBase.GetChild(0).gameObject.SetActive(false);
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            lightBase.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }   
}
