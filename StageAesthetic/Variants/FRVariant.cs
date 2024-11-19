using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    public class FRVariant: Variant
    {
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Apply(scenename, volume.GetComponent<FRCSharp.TheCoolerRampFog>(), fog, cgrade, volume, loop);
        }
        public virtual void Apply(string scenename, object fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop) { }
    }
}
