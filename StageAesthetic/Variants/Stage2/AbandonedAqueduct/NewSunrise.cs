using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class NewSunrise : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => "New Sunrise";
        public override string Description => "Rainy blue sky with more fog.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 129);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(68, 74, 86, 63);
            CaveFog.fogColorMid.value = new Color32(73, 83, 96, 100);
            CaveFog.fogColorEnd.value = new Color32(80, 89, 103, 200);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sunTransform).GetComponent<Light>();
            newSun.color = new Color32(125, 125, 255, 255);
            // 0.226 0.2148 0.6638 1
            newSun.intensity = 2f;
            newSun.shadowStrength = 0.25f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            Common.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Assets.ReplaceAll<Light>([new(["CrystalLight"], l => l.color = new Color(0.761f, 0.821f, 0.926f))]);
            Weather.AddRain(Intensity.Medium);
        }
    }
}
