using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public class Night : Variant
    {
        public override string[] Stages => ["goolake"];
        public override string Name => nameof(Night);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(60, 23, 72, 125);
            fog.fogColorMid.value = new Color32(30, 45, 70, 100);
            fog.fogColorEnd.value = new Color32(37, 41, 56, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogOne.value = 0.082f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(37, 46, 67, 115);
            CaveFog.fogColorMid.value = new Color32(37, 45, 57, 167);
            CaveFog.fogColorEnd.value = new Color32(38, 42, 55, 255);
            cgrade.colorFilter.value = new Color32(140, 164, 221, 255);
            cgrade.colorFilter.overrideState = true;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            //base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var shittyNotWorkingSun = lightBase.Find("Directional Light (SUN)");
            var sun2 = Object.Instantiate(shittyNotWorkingSun);
            shittyNotWorkingSun.gameObject.SetActive(false);
            var sunLight = sun2.GetComponent<Light>();
            sunLight.color = new Color32(197, 208, 207, 255);
            sunLight.intensity = 0.82f;
            sunLight.shadowStrength = 0.6f;
            sun2.localEulerAngles = new Vector3(42, 12, 180);
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
