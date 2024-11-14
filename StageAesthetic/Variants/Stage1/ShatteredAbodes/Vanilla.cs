using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.ShatteredAbodes
{
    public class Vanilla : Variant
    {
        public override string[] Stages => ["village"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
    }
}
