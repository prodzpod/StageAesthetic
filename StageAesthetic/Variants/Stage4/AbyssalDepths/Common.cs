using UnityEngine;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public static class Common
    {

        public static void SimMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material boulderMat)
        {
            Assets.MeshReplaceAll([
                new(["Mesh", "Ruin"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(mr => {
                    var parent = mr.transform.parent;
                    if (!parent) return false;
                    return (mr.gameObject.name.Contains("Mesh") && parent.gameObject.name.Contains("Ruin"))
                        || (mr.gameObject.name.Contains("RuinBowl") && parent.gameObject.name.Contains("RuinMarker"));
                }, mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(["Hero", "Ceiling"], mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(["Boulder"], mr => Assets.TryMeshReplace(mr, boulderMat)),
                new(mr => mr.gameObject.name.Contains("GiantRock") && !mr.gameObject.name.Contains("Slab"), mr => Assets.TryMeshReplace(mr, boulderMat)),
                new(["mdlGeyser", "Coral", "Heatvent", "Pebble", "Stalagmite"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["Ruin"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(["Column"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["Crystal"], mr => Assets.TryMeshReplace(mr, detailMat)),
                new(["LightMesh"], mr => {
                    if (mr.transform.childCount >= 1 && mr.transform.GetChild(0).name.Contains("Crystal"))
                        mr.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = detailMat;
                }),
                new(["Spike"], mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(["DCGiantRockSlab", "GiantStoneSlab", "TerrainBackwall", "Chain", "Wall"], mr => Assets.TryMeshReplace(mr, terrainMat)),
            ]);
        }
        public static void LightChange(Color coral, Color chain, Color crystal)
        {
            Assets.ReplaceAll<Light>([
                new(l => {
                    var parent = l.transform.parent;
                    if (!parent) return false;
                    return parent.gameObject.name.Equals("DCCoralPropMediumActive");
                }, l => {
                    l.color = coral;
                    var lightLP = l.transform.localPosition;
                    lightLP.z = 4;
                }),
                new(l => {
                    var parent = l.transform.parent;
                    if (!parent) return false;
                    return parent.gameObject.name.Equals("DCCrystalCluster Variant");
                }, l => l.color = crystal),
                new(l => l.gameObject.name.Equals("CrystalLight"), l => l.color = chain)
            ]);
        }
    }
}
