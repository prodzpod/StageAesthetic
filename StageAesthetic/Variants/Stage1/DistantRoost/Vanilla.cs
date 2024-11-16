using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["blackbeach", "blackbeach2"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Common.VanillaFoliage();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Mild);
    }
}
