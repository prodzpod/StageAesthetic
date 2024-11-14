using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class Sunrise : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => nameof(Sunrise);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 179);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(68, 74, 86, 63);
            CaveFog.fogColorMid.value = new Color32(73, 83, 96, 164);
            CaveFog.fogColorEnd.value = new Color32(80, 89, 103, 255);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(166, 221, 253, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            AbandonedAqueduct.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Assets.ReplaceAll<Light>([new(["CrystalLight"], l => l.color = new Color(0.761f, 0.821f, 0.926f))]);
            Weather.AddRain(Intensity.Medium);
        }
    }
}
