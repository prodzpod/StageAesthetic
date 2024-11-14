using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Nostalgic : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Nostalgic);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            volume.profile = Assets.Load<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGolemplains.asset");
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(0.7450981f, 0.8999812f, 0.9137255f);
            sun.intensity = 1.34f;

            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            VanillaFoliage();
        }

        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmGPGrass_LOD0"], mr => mr.sharedMaterial.color = new Color32(96, 94, 32, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                })
            ]);
        }
    }
}
