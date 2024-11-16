using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Bonus.VoidLocus
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["voidstage"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Void;
    }   
}
