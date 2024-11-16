using UnityEngine;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public static class Common
    {
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
