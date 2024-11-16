using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.SulfurPools
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["sulfurpools"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.WaterStream;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Common.VanillaWater();
        }
    }   
}
