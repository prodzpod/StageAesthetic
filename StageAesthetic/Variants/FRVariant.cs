using FRCSharp;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    public class FRVariant: Variant
    {
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Apply(scenename, volume.GetComponent<TheCoolerRampFog>(), fog, cgrade, volume, loop);
        }
        public virtual void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop) { }
    }
}
