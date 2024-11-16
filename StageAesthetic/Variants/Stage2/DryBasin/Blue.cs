using FRCSharp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.DryBasin
{
    public class Blue : FRVariant
    {
        public override string[] Stages => ["drybasin"];
        public override string Name => nameof(Blue);
        public override string Description => "Blue.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, fog2, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(149, 163, 217, 255);

            fog.startColor = new Color32(119, 158, 189, 11);
            fog.middleColor = new Color32(59, 135, 170, 35);
            fog.endColor = new Color32(148, 186, 221, 60);

            fog.fogZero = -0.005f;
            fog.fogOne = 0.09f;
            fog.power = 1f;
            fog.intensity = 0.8f;
            fog.skyboxPower = 1f;

            fog2.fogColorStart.value = new Color32(119, 158, 189, 11);
            fog2.fogColorMid.value = new Color32(59, 135, 170, 35);
            fog2.fogColorEnd.value = new Color32(148, 186, 221, 60);

            fog2.fogZero.value = -0.005f;
            fog2.fogOne.value = 0.09f;
            fog2.fogPower.value = 1f;
            fog2.fogIntensity.value = 0.8f;
            fog2.skyboxStrength.value = 1f;

            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(118, 129, 183, 255);
        }
    }
}
