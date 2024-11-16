using StageAesthetic.Variants.Stage3.SulfurPools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Blue : Variant
    {
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Blue);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(48, 102, 102, 81); // A cyan-ish color
            fog.fogColorMid.value = new Color32(61, 87, 94, 93);    // A darker cyan
            fog.fogColorEnd.value = new Color32(32, 142, 121, 200); // A bluish-green, which complements red
            fog.fogOne.value = 0.3f;
            fog.fogIntensity.value = 0.65f;
            fog.fogZero.value = -0.02f;

            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(229, 214, 255, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.6f;
            sunLight.transform.eulerAngles = new Vector3(65f, 222.6395f, 202.9964f);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(78, 55, 80, 60);
            caveFog.fogColorMid.value = new Color32(64, 75, 94, 144);
            caveFog.fogColorEnd.value = new Color32(60, 73, 88, 205);
            SimMaterials(
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainWallsInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianRuinsHeavyInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat")
            );
            // Lighting: Magenta coral, orange otherwise
            LightChange(new Color32(30, 209, 27, 255), new Color(0.981f, 0.521f, 0.065f), new Color(0.718f, 0, 0.515f));
        }
        public static void SimMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material boulderMat)
        {
            Assets.MeshReplaceAll([
                new(["Mesh", "Ruin"], mr => Assets.TryMeshReplace(mr, detailMat3)),
                new(mr => {
                    var parent = mr.transform.parent.gameObject;
                    if (!mr) return false;
                    return (mr.gameObject.name.Contains("Mesh") && parent.name.Contains("Ruin"))
                        || (mr.gameObject.name.Contains("RuinBowl") && parent.name.Contains("RuinMarker"));
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
                    var parent = l.transform.parent.gameObject;
                    if (!parent) return false;
                    return parent.name.Equals("DCCoralPropMediumActive");
                }, l => {
                    l.color = coral;
                    var lightLP = l.transform.localPosition;
                    lightLP.z = 4;
                }),
                new(l => {
                    var parent = l.transform.parent.gameObject;
                    if (!parent) return false;
                    return parent.name.Equals("DCCrystalCluster Variant");
                }, l => l.color = crystal),
                new(l => l.gameObject.name.Equals("CrystalLight"), l => l.color = chain)
            ]);
        }
    }   
}
