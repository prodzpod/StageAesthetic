using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.VerdantFalls
{
    public class Vanilla : Variant
    {
        public override string[] Stages => ["lakes", "lakesnight"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.WaterStream;
    }
}
