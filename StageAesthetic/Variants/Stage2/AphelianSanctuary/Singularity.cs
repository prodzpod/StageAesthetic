using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AphelianSanctuary
{
    public class Singularity : Variant
    {
        public override string[] Stages => ["ancientloft"];
        public override string Name => nameof(Singularity);
        public override string Description => "Very blue and dark.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.SingularitySky();
            Assets.TryDestroy("Directional Light (SUN)");
            Assets.TryDestroy("AL_Sun");
            Assets.TryDestroy("HOLDER: Cards");
            Assets.TryDestroy("Passing Cloud");
            VanillaFoliage();
        }
        public static void VanillaFoliage()
            => Assets.MeshReplaceAll([new(["spmBonsai1V1_LOD0", "spmBonsai1V2_LOD0"], mr =>
            {
                if (!mr.sharedMaterial) return;
                mr.sharedMaterial.color = new Color32(195, 195, 195, 255);
                if (mr.sharedMaterials.Length >= 4) mr.sharedMaterials[3].color = new Color32(195, 195, 195, 255);
            })]);
    }
}
