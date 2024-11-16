using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.DryBasin
{
    public class Vanilla: FRVariant
    {
        public override string[] Stages => ["drybasin"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Medium);
    }
}
