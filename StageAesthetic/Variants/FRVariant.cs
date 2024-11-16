using FRCSharp;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    public class FRVariant: Variant
    {
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            // get this
            Apply(scenename, fog, fog, cgrade, volume, loop);
        }
        public virtual void Apply(string scenename, TheCoolerRampFog fog, RampFog fog2, ColorGrading cgrade, PostProcessVolume volume, bool loop) { }
    }
}
