using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.SiphonedForest
{
    public class Crimson : Variant
    {
        public override string[] Stages => ["snowyforest"];
        public override string Name => nameof(Crimson);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(72, 18, 10, 46);
            fog.fogColorMid.value = new Color32(134, 0, 1, 131);
            fog.fogColorEnd.value = new Color32(107, 31, 27, 191);
            fog.SetAllOverridesTo(true);
            fog.skyboxStrength.value = 0.01f;
            fog.fogPower.value = 1f;
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.02f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(200, 175, 150, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.5f;
            sunLight.transform.eulerAngles = new Vector3(55f, 0f, 0f);

            var skybox = GameObject.Find("HOLDER: Skybox").transform;
            Assets.TryDestroy(skybox, "Godrays");
            Assets.TryDestroy(skybox, "SFPortalCard");
            Assets.TryDestroy(skybox, "SFPortalCard (1)");
            SiphonedForest.Vanilla.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        { SiphonedForest.Vanilla.DisableSiphonedSnow(); Weather.AddRain(Intensity.Extreme, true); }
    }
}
