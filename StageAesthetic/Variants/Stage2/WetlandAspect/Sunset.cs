using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.WetlandAspect
{
    public class Sunset : Variant
    {
        public override string[] Stages => ["foggyswamp"];
        public override string Name => nameof(Sunset);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 150);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(1f, 0.75f, 0.75f, 1f);
            sunLight.intensity = 2f;

            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(127, 124, 84, 0);
            caveOuter.fogColorMid.value = new Color32(188, 163, 47, 88);
            caveOuter.fogColorEnd.value = new Color32(162, 123, 46, 200);
            caveInner.fogColorStart.value = new Color32(162, 192, 5, 0);
            caveInner.fogColorMid.value = new Color32(149, 154, 89, 89);
            caveInner.fogColorEnd.value = new Color32(217, 201, 11, 200);
        }
    }
}
