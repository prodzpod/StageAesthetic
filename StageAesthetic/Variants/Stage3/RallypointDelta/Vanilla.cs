using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["frozenwall"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            DisableRallypointSnow();
            Weather.AddSnow(Intensity.Medium);
        }
        public static void DisableRallypointSnow()
        {
            var snowParticles = GameObject.Find("CAMERA PARTICLES: SnowParticles").gameObject;
            snowParticles.SetActive(false);
        }
    }
}
