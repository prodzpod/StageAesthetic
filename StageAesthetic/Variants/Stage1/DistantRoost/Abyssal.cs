using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Abyssal : Variant
    {
        public override string[] Stages => ["blackbeach2"];
        public override string Name => nameof(Abyssal);
        public override string Description => "Texture swap to Red Abyssal Depths.";
        public override SoundType Ambience => SoundType.WaterStream;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(99, 27, 63, 25);
            fog.fogColorMid.value = new Color32(26, 61, 91, 150);
            fog.fogColorEnd.value = new Color32(68, 27, 27, 255);
            fog.SetAllOverridesTo(true);
            fog.skyboxStrength.value = 0.1f;
            fog.fogPower.value = 1f;
            fog.fogIntensity.value = 1f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.2f;
            //  cgrade.colorFilter.value = new Color32(150, 150, 150, 255);
            // cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(0.75f, 0.75f, 0.75f, 1f);
            sunLight.intensity = 3f;
            sunLight.shadowStrength = 0.75f;
            sunLight.transform.eulerAngles = new Vector3(70f, 220f, -9.985f);
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;

            Assets.MeshReplaceAll([
                new(["Boulder", "boulder", "Rock", "Step", "Tile", "mdlGeyser", "Bowl", "Marker", "RuinPillar", "DistantBridge"], mr => {
                    var light = mr.gameObject.AddComponent<Light>();
                    light.color = new Color32(249, 212, 96, 255);
                    light.intensity = 10f;
                    light.range = 25f;
                }),
                new(["RuinGate", "RuinArch"], mr => {
                    var light = mr.gameObject.AddComponent<Light>();
                    light.color = new Color32(181, 66, 34, 225);
                    light.intensity = 7.5f;
                    light.range = 15f;
                }),
                new(mr => mr.gameObject.name == "spmBbConif_LOD2", mr => mr.gameObject.SetActive(false)),
            ]);
            Assets.ReplaceAll<Light>([new(l => {
                var lightParent = l.transform.parent;
                if (!lightParent) return false;
                return lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)");
            }, l => {
                l.color = new Color32(249, 212, 96, 225);
                l.intensity = 16f;
                l.range = 35f;
            })]);
            AbyssalFoliage();
            AbyssalMaterials();
        }
        public static void AbyssalFoliage()
        {
            Assets.MeshReplaceAll([
                new(["bbSimpleGrassPrefab"], mr => {
                    mr.sharedMaterial.color = new Color32(45, 45, 45, 211);
                    mr.transform.localScale = new Vector3(5.28f, 3.798217104f, 5.28f);
                }),
                new(["spmBbFern"], mr => {
                    mr.sharedMaterial.color = new Color32(50, 50, 50, 166);
                    mr.transform.localScale = new Vector3(3f, 3f, 3f);
                }),
                new(["spmBush"], mr => {
                    mr.sharedMaterial.color = new Color32(30, 30, 30, 255);
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(50, 50, 50, 255);
                }),
                new(["spmBbDryBush"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(20, 20, 20, 255);
                }),
                new(["Ivy"], mr => {
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(58, 58, 58, 146);
                    mr.sharedMaterials[1].color = new Color32(119, 119, 119, 133);
                }),
                new(["Vine"], mr => mr.sharedMaterial.color = new Color32(79, 63, 60, 255)),
                new(["spmBbConif_"], mr => {
                    mr.gameObject.SetActive(true);
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(100, 75, 75, 255);
                    mr.sharedMaterials[2].color = new Color32(100, 100, 100, 255);
                }),
                new(["spmBbConifYoung_L"], mr => {
                    mr.gameObject.SetActive(true);
                    foreach (var a in mr.sharedMaterials) a.color = new Color32(70, 70, 70, 255);
                    mr.sharedMaterials[2].color = new Color32(65, 68, 65, 255);
                }),
            ]);
        }
        public static void AbyssalMaterials()
        {
            Assets.MeshReplaceAll([
                new(mr => {
                    var meshParent = mr.transform.parent;
                    return meshParent && meshParent.gameObject.name.Contains("Pillar") && meshParent.Find("Foam") != null;
                }, mr => mr.transform.parent.Find("Foam").gameObject.SetActive(false)),
                new(mr => mr.transform.parent && mr.transform.parent.gameObject.name.Contains("terrain") && mr.gameObject.name.Contains("Pillar"),
                    mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat", new Color32(0, 0, 0, 204)))),
                new(mr => mr.transform.parent && mr.transform.parent.gameObject.name.Equals("Foliage") && mr.gameObject.name.Contains("bbSimpleGrassPrefab"),
                    mr => mr.gameObject.SetActive(false)),
                new(["Terrain", "Shelf"], mr => Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat", new Color32(0, 0, 0, 204))),
                new(["Boulder", "boulder", "Rock", "Step", "Tile", "mdlGeyser", "Bowl", "Marker", "DistantBridge", "Pebble"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/dampcavesimple/matDCBoulder.mat"))),
                new(["RuinGate", "RuinArch", "RuinPillar"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"))),
                new(["DistantPillar", "Cliff", "ClosePillar"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainWalls.mat", new Color32(0, 0, 0, 135)))),
                new(["Decal", "spmBbFern2"], mr => mr.gameObject.SetActive(false)),
                new(["GlowyBall"], mr => mr.sharedMaterial.color = new Color32(109, 58, 119, 140))
            ]);
            var distantRoostAbyssalWaterMat = Assets.LoadRecolor("RoR2/Base/goldshores/matGSWater.mat", new Color32(107, 23, 23, 255));
            distantRoostAbyssalWaterMat.shaderKeywords = ["_BUMPLARGE_ON", "_DISPLACEMENTMODE_OFF", "_DISPLACEMENT_ON", "_DISTORTIONQUALITY_HIGH", "_EMISSION", "_FOAM_ON", "_NORMALMAP"];
            GameObject.Find("HOLDER: Water").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = distantRoostAbyssalWaterMat;
        }
    }
}
