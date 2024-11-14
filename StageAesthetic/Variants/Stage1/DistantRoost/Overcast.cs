using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["blackbeach", "blackbeach2"];
        public override string Name => nameof(Overcast);
        public override string Description => "Rainy with more fog.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorEnd.value = new Color(0.3272f, 0.3711f, 0.4057f, 0.95f);
            fog.fogColorMid.value = new Color(0.2864f, 0.2667f, 0.3216f, 0.55f);
            fog.fogColorStart.value = new Color(0.2471f, 0.2471f, 0.2471f, 0.05f);
            fog.fogPower.value = 2f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.025f;
            fog.skyboxStrength.value = 0f;
            fog.fogIntensity.value = 1f;

            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.7f;
            sunLight.shadowStrength = 0.6f;
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;

            GameObject wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 0.5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;

            if (scenename == "blackbeach")
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);

            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];

            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;

                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 10;
                            light.range = 30;
                        }
                    }
                }
            }
            DistantRoost.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Extreme);
    }
}
