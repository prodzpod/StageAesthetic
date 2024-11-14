using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class NewNight : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => "New Night";
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(37, 46, 67, 75);
            CaveFog.fogColorMid.value = new Color32(37, 45, 57, 127);
            CaveFog.fogColorEnd.value = new Color32(38, 42, 55, 200);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Weather, Goolake").SetActive(false);
            Assets.ReplaceAll<Light>([new(["CrystalLight"], l => l.color = new Color(0.221f, 0.961f, 0.925f))]);
            AbandonedAqueduct.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Weather.AddRain(Intensity.Heavy);
            Weather.AddSand(Intensity.Extreme);
        }
    }
}
