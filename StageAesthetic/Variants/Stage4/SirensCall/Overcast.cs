using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.SirensCall
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["shipgraveyard"];
        public override string Name => nameof(Overcast);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(58, 62, 68, 0);
            fog.fogColorMid.value = new Color32(46, 67, 76, 130);
            fog.fogColorEnd.value = new Color32(78, 94, 87, 255);
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.057f;
            Night.ChangeGrassColor();
        }
    }   
}
