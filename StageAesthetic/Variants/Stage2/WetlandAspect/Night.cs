using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.WetlandAspect
{
    public class Night : Variant
    {
        public override string[] Stages => ["foggyswamp"];
        public override string Name => nameof(Night);
        public override string Description => "Green and dark.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky(true, true);

            fog.fogColorStart.value = new Color32(64, 65, 76, 15);
            fog.fogColorMid.value = new Color32(48, 53, 66, 173);
            fog.fogColorEnd.value = new Color32(30, 30, 58, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogOne.value = 0.25f;
            fog.fogZero.value = -0.02f;
            fog.fogPower.value = 0.9f;
            fog.fogIntensity.value = 0.937f;
            fog.fogOne.value = 0.355f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(109, 126, 185, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.7f;

            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(14, 111, 160, 0);
            caveOuter.fogColorMid.value = new Color32(66, 76, 43, 89);
            caveOuter.fogColorEnd.value = new Color32(75, 84, 51, 255);
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Medium);
    }
}
