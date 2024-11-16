using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public class Sunset : Variant
    {
        public override string[] Stages => ["frozenwall"];
        public override string Name => nameof(Sunset);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 28, 150);
            fog.fogColorEnd.value = new Color32(160, 74, 61, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var sun = GameObject.Find("Directional Light (SUN)");
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.active = false;
            SunsetMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Weather.AddSnow(Intensity.Mild);
        }
        public static void SunsetMaterials()
        {
            GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = 
                Assets.Load<Material>("RoR2/DLC1/sulfurpools/matSPWaterYellow.mat");
        }
    }
}
