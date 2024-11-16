using RoR2;
using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public class Titanic : Variant
    {
        public override string[] Stages => ["frozenwall"];
        public override string Name => nameof(Titanic);
        public override string Description => "Texture swap to Titanic Plains.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(116, 153, 173, 4);
            fog.fogColorMid.value = new Color32(88, 130, 153, 40);
            fog.fogColorEnd.value = new Color32(77, 127, 152, 255);
            fog.skyboxStrength.value = 0f;
            // cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            // cgrade.colorFilter.overrideState = true;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.name = "Directional Light (SUN)";
            sunLight.color = new Color32(255, 212, 153, 255);
            sunLight.intensity = 1.4f;
            sunLight.shadowStrength = 0.7f;
            var yellow = new Color32(255, 185, 0, 255);
            Assets.ReplaceAll<Light>([
                new(l => !l.gameObject.name.Contains("Light (SUN)"), l => {
                    l.color = new Color32(255, 185, 0, 255);
                    l.intensity = 0.08f;
                    l.range = 4f;
                }),
                new(l => l.color == yellow, l => {
                    if (l.GetComponent<FlickerLight>()) l.GetComponent<FlickerLight>().enabled = false;
                    l.range = 100f;
                    l.intensity = 3f;
                })
            ]);
            GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
            GameObject.Find("STATIC PARTICLES: Cave Entrance System").SetActive(false);
            // GameObject.Find("HOLDER: ShippingCenter").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Stalactite").SetActive(false);
            GameObject.Find("HOLDER: Stalagmites").SetActive(false);

            var bloom = volume.profile.GetSetting<Bloom>();
            bloom.intensity.value = 0f;

            TitanicMaterials();
        }
        public static void TitanicMaterials()
        {
            var rpdTitanicTerrainMat = Assets.LoadRecolor("RoR2/Base/golemplains/matGPTerrain.mat", new Color32(95, 96, 132, 232));
            rpdTitanicTerrainMat.SetFloat("_Depth", 0.1740239f);
            rpdTitanicTerrainMat.SetFloat("_BlueChannelBias", 0.9805416f);
            var rpdTitanicDetailMat = Assets.LoadRecolor("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat", new Color32(76, 90, 115, 78));
            rpdTitanicDetailMat.SetFloat("_SpecularStrength", 0.009451796f);
            rpdTitanicDetailMat.SetFloat("_Depth", 0.135765f);
            var rpdTitanicDetailMat2 = Assets.LoadRecolor("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuinsProjectedHuge.mat", new Color32(209, 171, 29, 198));
            rpdTitanicDetailMat2.SetFloat("_NormalStrength", 0.1499685f);
            rpdTitanicDetailMat2.SetFloat("_SpecularStrength", 0.227f);
            rpdTitanicDetailMat2.SetFloat("_SpecularExponent", 5.497946f);
            rpdTitanicDetailMat2.SetFloat("_Smoothness", 0.4f);
            rpdTitanicDetailMat2.SetFloat("_SnowSpecularStrength", 0.1436673f);
            rpdTitanicDetailMat2.SetFloat("_SnowSpecularExponent", 0.9451796f);
            rpdTitanicDetailMat2.SetFloat("_SnowSmoothness", 1f);
            rpdTitanicDetailMat2.SetFloat("_SnowBias", -0.7378702f);
            rpdTitanicDetailMat2.SetFloat("_Depth", 0.07435415f);
            rpdTitanicDetailMat2.SetFloat("_TriplanarTextureFactor", 0.4f);
            GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/Base/goldshores/matGSWater.mat");
            Assets.MeshReplaceAll([
                new(["Terrain", "Snow", "FW_FloatingIsland"], mr => Assets.TryMeshReplace(mr, rpdTitanicTerrainMat)),
                new(["Glacier", "Stalagmite", "Boulder", "CavePillar", "FW_Pillar"], mr => Assets.TryMeshReplace(mr, rpdTitanicDetailMat)),
                new(["GroundMesh", "GroundStairs", "VerticalPillar", "Human", "Barrier", "FW_Ground", "FW_WaterContainer", "FW_Canister", "ShippingContainer", "ArtifactFormulaHolderMesh", "FW_Crate"], mr => Assets.TryMeshReplace(mr, rpdTitanicDetailMat2)),
                new(mr => mr.gameObject.name.Contains("Pillar") && mr.transform.parent && mr.transform.parent.name.Contains("VerticalPillarParent"), mr => Assets.TryMeshReplace(mr, rpdTitanicDetailMat2)),
                new(["HumanChainLink", "Stalactite"], mr => mr.gameObject.SetActive(false))
            ]);
        }
    }
}
