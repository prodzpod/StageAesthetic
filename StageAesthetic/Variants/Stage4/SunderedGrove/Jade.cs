using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.SunderedGrove
{
    public class Jade : Variant
    {
        public override string[] Stages => ["rootjungle"];
        public override string Name => nameof(Jade);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(70, 90, 84, 0);
            fog.fogColorMid.value = new Color32(74, 99, 105, 100);
            fog.fogColorEnd.value = new Color32(77, 113, 85, 150);
            fog.skyboxStrength.value = 0f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(152, 255, 255, 255);
            sunLight.intensity = 2f;
            sunTransform.localEulerAngles = new Vector3(60, 15, -4);
            SunderedGrove.Vanilla.VanillaFoliage();
        }
    }   
}
