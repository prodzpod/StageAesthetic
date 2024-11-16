using UnityEngine;

namespace StageAesthetic.Variants.Stage1.ShatteredAbodes
{
    public static class Common
    {

        public static void AbodesMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            Assets.MeshReplaceAll([
                new(["Grass", "Fern"], mr => Object.Destroy(mr.gameObject)),
                new(["HouseBuried", "LVTerrain", "LVArc_StormOutlook", "BuriedHouse"], mr => { if (mr.sharedMaterials.Length == 2) mr.sharedMaterials = [terrainMat, detailMat2]; }),
                new(["LVTerrainToggle", "LVTerrainFar", "Dune", "BrokenAltar", "LVTerrainBackground"], mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(["LVArc_Temple", "LVArc_Houses", "LVArc_CliffCave", "LVArc_Bridge", "LVArc_BrokenPillar"], mr => { if (mr.sharedMaterials.Length == 2) mr.sharedMaterials = [detailMat2, terrainMat]; }),
                new(["Pillar"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["RockMedium", "Pebble"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["Crystal"], mr => Assets.TryMeshReplace(mr, detailMat3)),
            ]);
        }
    }
}
