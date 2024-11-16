using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.SiphonedForest
{
    public class Night : Variant
    {
        public override string[] Stages => ["snowyforest"];
        public override string Name => nameof(Night);
        public override string Description => "Blue and dark.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky(true);
            fog.fogColorStart.value = new Color32(112, 125, 166, 50);
            fog.fogColorMid.value = new Color32(80, 80, 110, 64);
            fog.fogColorEnd.value = new Color32(42, 42, 72, 249);
            fog.skyboxStrength.value = 0.3f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            var godrays = GameObject.Find("Godrays");
            godrays.SetActive(false);
            sunLight.color = new Color32(110, 110, 180, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(110, 110, 140, 25);
            cgrade.colorFilter.overrideState = true;
            Common.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        { Common.DisableSiphonedSnow(); Weather.AddSnow(Intensity.Heavy); }
    }
}
