using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AphelianSanctuary
{
    public class Sunset : Variant
    {
        public override string[] Stages => ["ancientloft"];
        public override string Name => nameof(Sunset);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            GameObject sun = GameObject.Find("AL_Sun");
            if (sun)
            {
                sun.SetActive(false);
                GameObject newSun = GameObject.Instantiate(Skybox.sun, sun.transform.parent);
                newSun.transform.localPosition = new Vector3(-897.0126f, 350f, 209.9904f);
                newSun.transform.eulerAngles = new Vector3(275f, 90f, 90f);
                newSun.GetComponent<MeshRenderer>().sharedMaterial = Skybox.sunMat;
            }
            Assets.TryDestroy("Directional Light (SUN)");
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 150);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;

            Assets.TryDestroy("DeepFog");
            Singularity.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Mild);
    }
}
