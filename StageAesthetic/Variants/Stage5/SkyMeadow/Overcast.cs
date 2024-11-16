using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Overcast);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(98, 94, 76, 0);
            fog.fogColorMid.value = new Color32(66, 65, 93, 139);
            fog.fogColorEnd.value = new Color32(72, 79, 95, 255);
            fog.fogZero.value = -0.02f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogOne.value = 0.1f;
            fog.skyboxStrength.value = 0.1f;

            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            var sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(202, 183, 142, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.6f;
            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
            GameObject.Find("SMSkyboxPrefab").transform.Find("SmallStars").gameObject.SetActive(false);
            SkyMeadow.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Extreme);
    }   
}
