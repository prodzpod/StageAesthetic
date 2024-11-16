using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["frozenwall"];
        public override string Name => nameof(Overcast);
        public override string Description => "Rainy and snowy with more fog.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(47, 52, 62, 50);
            fog.fogColorMid.value = new Color32(72, 80, 98, 165);
            fog.fogColorEnd.value = new Color32(90, 101, 119, 255);
            fog.skyboxStrength.value = 0.15f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.4f;

            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.color = new Color32(177, 205, 232, 255);
            sunLight.intensity = 0.5f;
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3(-1260, -66, 0);
            sunLight.color = new Color32(155, 174, 200, 255);
            sunLight.intensity = 1.3f;
            sunLight.name = "Directional Light (SUN)";


            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.intensity.value = 0.7f;
            bloom.threshold.value = 0.39f;
            bloom.softKnee.value = 0.7f;
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Weather.AddRain(Intensity.Heavy);
            Common.DisableRallypointSnow();
            Weather.AddSnow(Intensity.Mild, 250f);
        }
    }
}
