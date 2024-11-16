using UnityEngine;

namespace StageAesthetic.Variants.Stage5.SlumberingSatellite
{
    public static class Common
    {
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["Edge Clouds"], mr => mr.gameObject.SetActive(false)),
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(236, 161, 182, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(236, 161, 182, 255);
                }),
                new(["SMVineBody"], mr => mr.sharedMaterial.color = new Color32(144, 158, 70, 255)),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(130, 150, 171, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                }),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
            ]);
        }
    }
}
