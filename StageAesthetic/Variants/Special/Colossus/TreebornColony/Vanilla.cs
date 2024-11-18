using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.TreebornColony
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["habitat", "habitatfall"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
    }   
}
