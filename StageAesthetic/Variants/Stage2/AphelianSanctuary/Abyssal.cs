using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AphelianSanctuary
{
    public class Abyssal : Variant
    {
        public override float PreLoopWeightDefault => 0;
        public override float LoopWeightDefault => 0;
        public override string[] Stages => ["ancientloft"];
        public override string Name => nameof(Abyssal);
        public override string Description => "Texture swap to Red Abyssal Depths. Kinda sucks right now.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var cloud = GameObject.Find("Cloud3");
            cloud.transform.localPosition = new Vector3(-22.8f, -138f, 46.7f);
            fog.fogColorStart.value = new Color32(102, 68, 68, 81);
            fog.fogColorMid.value = new Color32(94, 71, 71, 93);
            fog.fogColorEnd.value = new Color32(124, 62, 62, 255);
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(150, 150, 150, 255);
            sunLight.intensity = 2.5f;
            // sunLight.shadowStrength = 0.4f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.transform.localPosition = new Vector3(0f, 110f, 0f);

            var cloud1 = fog1.transform.GetChild(1);
            cloud1.transform.localPosition = new Vector3(87.5f, -66f, 0f);
            cloud1.transform.localScale = new Vector3(120f, 120f, 120f);

            for (int i = 0; i < 5; i++)
            {
                var instantiated = Object.Instantiate(cloud1.gameObject);
                instantiated.transform.localPosition = new Vector3(87.5f + (2.5f * i), -10f + (i * 12f), 0f - (i * 5f));
                instantiated.transform.localScale = new Vector3(120f, 120f, 120f);
            }

            var fuckYou = fog1.transform.GetChild(0);
            fuckYou.transform.localPosition = new Vector3(-38.4f, -66f, -7.5f);

            var sun = GameObject.Find("Sun");
            sun.SetActive(false);
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var stupidList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];

            Assets.MeshReplaceAll([new(["CirclePot", "BrokenPot", "Planter", "AW_Cube", "Mesh, Cube", "AL_WaterFenceType", "Tile", "Rock", "Pillar", "Boulder", "Step", "LightStatue", "LightStatue_Stone", "FountainLG", "Shrine", "Sculpture"], mr => {
                    if (!mr.transform.parent) return;
                    var light = mr.gameObject.AddComponent<Light>();
                    light.color = new Color32(125, 43, 48, 225);
                    light.intensity = 6f;
                    light.range = 24f;
            })]);
            Assets.ReplaceAll<SkinnedMeshRenderer>([new(["CirclePot", "Planter", "AW_Cube", "Mesh, Cube", "AL_WaterFenceType", "Tile", "RuinBlock", "Rock", "Pillar", "Boulder", "Step", "LightStatue", "LightStatue_Stone", "FountainLG", "Shrine", "Sculpture"], mr => {
                var light = mr.gameObject.AddComponent<Light>();
                light.color = new Color32(249, 212, 96, 225);
                light.intensity = 6f;
                light.range = 24f;
            })]);
            AbyssalFoliage();
            AbyssalMaterials();
        }
        public static void AbyssalFoliage()
            => Assets.MeshReplaceAll([new(["spmBonsai1V1_LOD0", "spmBonsai1V2_LOD0"], mr => {
                if (!mr.sharedMaterial) return;
                mr.sharedMaterial.color = new Color32(134, 53, 255, 255);
                if (mr.sharedMaterials.Length >= 4) mr.sharedMaterials[3].color = new Color32(134, 53, 255, 255);
            })]);
        public static void AbyssalMaterials()
        {
            var terrainMat = Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat", new Color32(0, 0, 0, 204));
            var detailMat = Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainWalls.mat", new Color32(0, 0, 0, 135));
            var detailMat2 = Assets.Load<Material>("RoR2/Base/dampcavesimple/matDCBoulder.mat");
            var detailMat3 = Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat");
            Assets.MeshReplaceAll([
                new(mr => {
                    var meshParent = mr.transform.parent; 
                    if (!meshParent) return false;
                    return meshParent.gameObject.name.Contains("TempleTop") && mr.gameObject.name.Contains("RuinBlock") || mr.gameObject.name.Contains("GPRuinBlockQuarter");
                }, mr => Assets.TryMeshReplace(mr, detailMat2)), // distantRoostAbyssalDetailMat
                new(mr => mr.gameObject.name.Equals("Terrain") && mr.sharedMaterials.Length > 0, mr => Assets.MeshReplaceAll(mr, terrainMat)),
                new(["Terrain", "Dirt", "TerrainPlatform"], mr => {
                    // if (mr.gameObject.name.Equals("Terrain")) return;
                    Assets.TryMeshReplace(mr, terrainMat);
                }),
                new(["Platform", "Temple", "Bridge"], mr => mr.sharedMaterials = [detailMat2, terrainMat]),
                new(["CirclePot", "BrokenPot", "Planter", "AW_Cube", "Mesh, Cube", "AL_WaterFenceType", "Pillar", "LightStatue", "FountainLG", "Shrine", "Sculpture", "AL_SculptureSM", "FountainSM"], 
                    mr => Assets.TryMeshReplace(mr, detailMat3)), // distantRoostAbyssalDetailMat2
                new(["Tile", "Step", "Rock", "Pebble", "Rubble", "Boulder", "LightStatue_Stone"], mr => Assets.TryMeshReplace(mr, detailMat3)), // distantRoostAbyssalTerrainMat2
                new(["CircleArchwayAnimatedMesh"], mr => {
                    Assets.MeshReplaceAll(mr, detailMat2); // distantRoostAbyssalDetailMat
                    mr.sharedMaterials[1] = detailMat3; // distantRoostAbyssalDetailMat2
                })
            ]);
            Assets.ReplaceAll<SkinnedMeshRenderer>([
                new(["CirclePot", "Planter", "AW_Cube", "Mesh, Cube", "AL_WaterFenceType", "RuinBlock", "Pillar", "LightStatue", "LightStatue_Stone", "FountainLG", "Shrine", "Sculpture"],
                    mr => mr.sharedMaterial = detailMat3),
                new(["Rock", "Tile", "Boulder", "Step", "Pebble", "Rubble"], mr => mr.sharedMaterial = detailMat)
            ]);
        }
    }
}
