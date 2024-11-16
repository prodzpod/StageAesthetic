using UnityEngine;

namespace StageAesthetic.Variants.Special.Colossus.ReformedAltar
{
    public static class Common
    {
        public static void AltarMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material grassMat2)
        {
            Assets.MeshReplaceAll([
                new(["GrassTall"], mr => Assets.TryMeshReplace(mr, grassMat2)),
                new(["LTTerrain", "Dune"], mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(["LTCeiling", "LTTemple", "Altar", "Arches", "LTColumn", "LTStairs"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["LTCeilingRoots", "Tube"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(["Gold", "Boulder", "Coral", "Crystal"], mr => Assets.TryMeshReplace(mr, detailMat)),
            ]);
        }
    }
}
