using UnityEngine;

namespace StageAesthetic.Variants.Stage4.SirensCall
{
    public static class Common
    {
        public static void ChangeGrassColor()
        {
            Assets.MeshReplaceAll([
                new(["Grass"], mr => {
                    if (mr.sharedMaterial) return;
                    mr.sharedMaterial.color = new Color32(99, 97, 63, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(99, 97, 63, 255);
                }),
                new(["DanglingMoss"], mr => { if (mr.sharedMaterial) mr.sharedMaterial.color = new Color32(255, 255, 255, 255); })
            ]);
        }
    }
}
