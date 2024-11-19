using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SlumberingSatellite
{
    public class Vanilla: FRVariant
    {
        public override string[] Stages => ["slumberingsatellite"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, object _fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, _fog, fog2, cgrade, volume, loop);
            Common.VanillaFoliage();
        }
    }
}
