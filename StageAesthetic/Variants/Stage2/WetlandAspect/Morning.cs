using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.WetlandAspect
{
    public class Morning : Variant
    {
        public override string[] Stages => ["foggyswamp"];
        public override string Name => nameof(Morning);
        public override string Description => "Blue and yellow.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.DaySky();
            fog.fogColorStart.value = new Color32(128, 121, 99, 13);
            fog.fogColorMid.value = new Color32(106, 141, 154, 130);
            fog.fogColorEnd.value = new Color32(104, 150, 199, 255);
            fog.fogZero.value = -0.058f;
            fog.fogPower.value = 1.2f;
            fog.fogIntensity.value = 0.937f;
            fog.skyboxStrength.value = 0.1f;
            cgrade.colorFilter.value = new Color32(240, 213, 248, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(0.9f, 0.9f, 1, 1);
            sunLight.intensity = 1.1f;
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(124, 86, 109, 0);
            caveOuter.fogColorMid.value = new Color32(154, 89, 127, 89);
            caveOuter.fogColorEnd.value = new Color32(227, 118, 219, 255);
            caveInner.fogColorStart.value = new Color32(131, 86, 94, 0);
            caveInner.fogColorMid.value = new Color32(137, 22, 24, 89);
            caveInner.fogColorEnd.value = new Color32(152, 8, 6, 255);
        }
    }
}
