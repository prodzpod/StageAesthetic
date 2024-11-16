using StageAesthetic.Variants.Stage1.DistantRoost;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Overcast);
        public override string Description => "Rainy with more fog.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(34, 45, 62, 18);
            fog.fogColorMid.value = new Color32(72, 84, 103, 165);
            fog.fogColorEnd.value = new Color32(97, 109, 129, 255);
            fog.skyboxStrength.value = 0.075f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(64, 144, 219, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(50, 17, 270);
            if (scenename == "golemplains")
            {
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 145, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 0.4f;
                windZone.windTurbulence = 0.8f;
            }
            Common.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Medium);
    }
}
