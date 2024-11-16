using FRCSharp;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SlumberingSatellite
{
    public class Blue : FRVariant
    {
        public override string[] Stages => ["slumberingsatellite"];
        public override string Name => nameof(Blue);
        public override string Description => "BLUE!";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, fog2, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(160, 255, 208, 255);
            sun.intensity = 1f;
            sun.shadowStrength = 0.6f;

            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.07f;
            fog.fogOne = 0.15f;
            fog.startColor = new Color32(69, 107, 134, 50);
            fog.middleColor = new Color32(94, 140, 135, 6);
            fog.endColor = new Color32(66, 84, 87, 255);
            fog.skyboxPower = 0f;

            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.07f;
            fog2.fogOne.value = 0.15f;
            fog2.fogColorStart.value = new Color32(69, 107, 134, 50);
            fog2.fogColorMid.value = new Color32(94, 140, 135, 6);
            fog2.fogColorEnd.value = new Color32(66, 84, 87, 255);
            fog2.skyboxStrength.value = 0f;
            Common.VanillaFoliage();
        }
    }
}
