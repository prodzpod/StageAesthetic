using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.WetlandAspect
{
    public class Void : Variant
    {
        public override string[] Stages => ["foggyswamp"];
        public override string Name => nameof(Void);
        public override string Description => "Texture swap to Purple Void Fields.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.VoidSky();
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(62, 12, 120, 0);
            caveOuter.fogColorMid.value = new Color32(66, 29, 132, 89);
            caveOuter.fogColorEnd.value = new Color32(187, 145, 238, 200);
            var terrain = GameObject.Find("HOLDER: Hero Assets").transform;
            terrain.GetChild(4).localPosition = new Vector3(-23.9f, -149.9f, 119f);
            GameObject.Find("HOLDER: Hidden Altar Stuff").transform.GetChild(1).gameObject.SetActive(false);
            var r = GameObject.Find("HOLDER: Ruin Pieces").transform;
            r.GetChild(22).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Foliage").SetActive(false);
            VoidMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSnow(Intensity.Mild);
        public static void VoidMaterials()
        {
            var s = GameObject.Find("HOLDER: Skybox").transform;
            var terHold = GameObject.Find("HOLDER: Hero Assets").transform;
            var terrain = terHold.Find("Terrain");
            var terrainMat = Assets.LoadRecolor("RoR2/Base/arena/matArenaTerrain.mat", new Color32(171, 167, 234, 132));
            var terrainMat2 = Assets.Load<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat");
            var detailMat = Assets.Load<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat");
            var detailMat2 = Assets.Load<Material>("RoR2/Base/arena/matArenaHeatvent1.mat");
            var detailMat3 = Assets.Load<Material>("RoR2/Base/arena/matArenaTrim.mat");
            var water = Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat", new Color32(82, 24, 109, 255));
            terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            terrain.GetChild(3).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            terrain.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            terHold.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            terHold.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = water;
            terrain.GetChild(4).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            s.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            GameObject.Find("HOLDER: Ruin Pieces").transform.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat3;
            Assets.MeshReplaceAll([
                new(["Boulder", "Pebble", "Blender", "Trunk", "Door", "Frame"], mr => {
                    if (!mr.sharedMaterial) return;
                    mr.sharedMaterial = detailMat;
                    if (mr.gameObject.transform.GetComponentInChildren<MeshRenderer>())
                        mr.gameObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = detailMat;
                }),
                new(mr => {
                    var parent = mr.transform.parent;
                    if (!parent) return false;
                    return mr.gameObject.name.Contains("Mesh") && (parent.gameObject.name.Contains("FSTree") || parent.gameObject.name.Contains("FSRootBundle"));
                }, mr => Assets.TryMeshReplace(mr, detailMat2)),
                new(mr => {
                    var parent = mr.transform.parent;
                    if (!parent) return false;
                    return mr.gameObject.name.Contains("Mesh") && parent.gameObject.name.Contains("FSRuinPillar");
                }, mr => Assets.TryMeshReplace(mr, detailMat)),
                new(mr => {
                    var parent = mr.transform.parent;
                    if (!parent) return false;
                    return (mr.gameObject.name.Contains("RootBundleLargeCards") || mr.gameObject.name.Contains("RootBundleSmallCards")) && (parent.gameObject.name.Contains("FSRootBundleLarge") || parent.gameObject.name.Contains("FSRootBundleSmall"));
                }, mr => mr.gameObject.SetActive(false)),
                new(["RootBundleLarge_LOD0", "RootBundleLarge_LOD1", "RootBundleLarge_LOD2", "RootBundleSmall_LOD0", "RootBundleSmall_LOD1", "RootBundleSmall_LOD2"], mr => {
                    var parent = mr.transform.parent;
                    if (!parent) return;
                    if (parent.gameObject.name.Contains("FSRootBundleLarge") || parent.gameObject.name.Contains("FSRootBundleSmall"))
                        Assets.TryMeshReplace(mr, detailMat);
                }),
                new(mr => mr.gameObject.name.Contains("Ruin") && mr.gameObject.name != "FSGiantRuinDoorCollision", mr => Assets.TryMeshReplace(mr, detailMat2))
            ]);
        }
    }
}
