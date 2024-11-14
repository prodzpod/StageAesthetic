using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class Dawn : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => nameof(Dawn);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 126);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1.1f;
            fog.fogPower.value = 0.8f;

            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            GameObject.Find("HOLDER: Warning Flags").SetActive(false);
            // base1.Find("Warning Signs").gameObject.SetActive(true);
            // NRE above
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
            newSun.intensity = 3f;
            newSun.color = new Color32(113, 45, 21, 255);
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(67, 35, 76, 65);
            CaveFog.fogColorMid.value = new Color32(41, 17, 51, 165);
            CaveFog.fogColorEnd.value = new Color32(84, 31, 20, 255);
            Assets.ReplaceAll<Light>([new(["CrystalLight"], l => l.color = new Color(0.721f, 0.041f, 0.065f))]);
            AbandonedAqueduct.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Mild);
    }
}
