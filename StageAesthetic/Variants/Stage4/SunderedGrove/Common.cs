using UnityEngine;

namespace StageAesthetic.Variants.Stage4.SunderedGrove
{
    public static class Common
    {
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["RJShroomFoliage_", "RJTreeBigFoliage_"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
                new(["RJMossFoliage_"], mr => mr.sharedMaterial.color = new Color32(122, 215, 221, 255)),
                new(["RJTowerTreeFoliage_"], mr => {
                    foreach (var m in mr.sharedMaterials) m.color = new Color32(171, 171, 171, 255);
                }),
                new(["RJHangingMoss_"], mr => mr.sharedMaterial.color = new Color32(105, 130, 110, 255)),
                new(["spmFern1_"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
                new(["spmRJgrass1_"], mr => mr.sharedMaterial.color = new Color32(190, 164, 179, 255)),
                new(["spmRJgrass2_"], mr => mr.sharedMaterial.color = new Color32(207, 207, 207, 255)),
            ]);
        }
    }
}
