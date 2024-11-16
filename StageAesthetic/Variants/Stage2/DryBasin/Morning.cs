using FRCSharp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.DryBasin
{
    public class Morning : FRVariant
    {
        public override string[] Stages => ["drybasin"];
        public override string Name => nameof(Morning);
        public override string Description => "Yellow sun with blue shadows.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, fog2, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(255, 243, 207, 255);
            sun2.intensity = 1f;
            sun2.shadowStrength = 0.8f;

            fog.skyboxPower = 1f;
            fog.power = 0.8f;
            fog.fogZero = -0.02f;
            fog.fogOne = 0.5f;
            fog.startColor = new Color32(0, 65, 255, 1);
            fog.middleColor = new Color32(226, 203, 255, 20);
            fog.endColor = new Color32(106, 0, 255, 53);

            fog2.skyboxStrength.value = 1f;
            fog2.fogPower.value = 0.8f;
            fog2.fogZero.value = -0.02f;
            fog2.fogOne.value = 0.5f;
            fog2.fogColorStart.value = new Color32(0, 65, 255, 1);
            fog2.fogColorMid.value = new Color32(226, 203, 255, 20);
            fog2.fogColorEnd.value = new Color32(106, 0, 255, 53);
        }
    }
}
