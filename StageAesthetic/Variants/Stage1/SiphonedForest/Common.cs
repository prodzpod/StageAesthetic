using UnityEngine;

namespace StageAesthetic.Variants.Stage1.SiphonedForest
{
    public static class Common
    {
        public static void DisableSiphonedSnow()
            => GameObject.Find("HOLDER: Skybox").transform.Find("CAMERA PARTICLES: SnowParticles").gameObject.SetActive(false);
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmGPGrass_LOD0"], mr => mr.sharedMaterial.color = new Color32(168, 168, 141, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(125, 125, 128, 255);
                })
            ]);
        }
    }
}
