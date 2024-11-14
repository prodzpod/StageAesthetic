using StageAesthetic.Variants.Stage1.DistantRoost;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Night : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Night);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSkyNoBullshit();
            fog.fogColorStart.value = new Color32(0, 0, 0, 0);
            fog.fogColorMid.value = new Color32(52, 73, 85, 34);
            fog.fogColorEnd.value = new Color32(12, 18, 54, 255);
            fog.skyboxStrength.value = 0.08f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 1f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.06f;
            cgrade.colorFilter.value = new Color32(180, 184, 255, 255);
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(113, 132, 255, 255);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(38, 270, 97);
            Nostalgic.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Mild);
    }
}
