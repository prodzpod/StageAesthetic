using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Crimson : Variant
    {
        public override float PreLoopWeightDefault => 0;
        public override float LoopWeightDefault => 0;
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Crimson);
        public override string Description => "Brings back the unreleased Scorched Acres' look (alt ver)";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 60);
            fog.fogColorEnd.value = new Color32(163, 74, 61, 120);
            fog.skyboxStrength.value = 0.62f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(191, 191, 191, 255);
            sunLight.intensity = 1.4f;
            sunLight.shadowStrength = 0.75f;
            sunTransform.localEulerAngles = new Vector3(65, 37, 0);
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.gameObject.SetActive(false);
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Heavy, true);
    }   
}
