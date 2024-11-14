using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Sunny: Variant
    {
        public override string[] Stages => ["blackbeach", "blackbeach2"];
        public override string Name => nameof(Sunny);
        public override string Description => "Yellow sun over greenery.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.DaySky();
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.8f;
            sunLight.shadowStrength = 0.75f;
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            if (scenename == "blackbeach")
            {
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
            }
            DistantRoost.Vanilla.VanillaFoliage();
        }
    }
}
