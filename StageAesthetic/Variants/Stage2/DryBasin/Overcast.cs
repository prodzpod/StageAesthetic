using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.DryBasin
{
    public class Overcast : FRVariant
    {
        public override string[] Stages => ["drybasin"];
        public override string Name => nameof(Overcast);
        public override string Description => "Overcast and rainy.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, object _fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, _fog, fog2, cgrade, volume, loop);
            var fog = (FRCSharp.TheCoolerRampFog)_fog;
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            sun2.shadows = LightShadows.Soft;

            fog.skyboxPower = 0f;
            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.01f;
            fog.fogOne = 0.13f;
            fog.startColor = new Color32(219, 138, 136, 15);
            fog.middleColor = new Color32(156, 128, 109, 105);
            fog.endColor = new Color32(201, 143, 131, 255);

            fog2.skyboxStrength.value = 0f;
            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.01f;
            fog2.fogOne.value = 0.13f;
            fog2.fogColorStart.value = new Color32(219, 138, 136, 15);
            fog2.fogColorMid.value = new Color32(156, 128, 109, 105);
            fog2.fogColorEnd.value = new Color32(201, 143, 131, 255);

            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(100, 109, 121, 255);
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Extreme);
    }
}
