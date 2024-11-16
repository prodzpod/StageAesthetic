using FRCSharp;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SlumberingSatellite
{
    public class Morning : FRVariant
    {
        public override string[] Stages => ["slumberingsatellite"];
        public override string Name => nameof(Morning);
        public override string Description => "Blue and yellow.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, fog2, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(255, 219, 160, 255);
            sun.intensity = 2f;
            sun.shadowStrength = 0.85f;

            /*
            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.05f;
            fog.fogOne = 0.2f;
            fog.startColor = new Color32(164, 232, 230, 3);
            fog.middleColor = new Color32(101, 132, 134, 6);
            fog.endColor = new Color32(89, 123, 132, 255);
            fog.skyboxPower = 0f;

            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.05f;
            fog2.fogOne.value = 0.2f;
            fog2.fogColorStart.value = new Color32(164, 232, 230, 3);
            fog2.fogColorMid.value = new Color32(101, 132, 134, 6);
            fog2.fogColorEnd.value = new Color32(89, 123, 132, 255);
            fog2.skyboxStrength.value = 0f;
            */
            Common.VanillaFoliage();
        }
    }
}
