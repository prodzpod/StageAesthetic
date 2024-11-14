using RoR2;
using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace StageAesthetic.Variants.Stage1.SiphonedForest
{
    public class Desolate : Variant
    {
        public override string[] Stages => ["snowyforest"];
        public override string Name => nameof(Desolate);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(206, 117, 255, 5);
            fog.fogColorMid.value = new Color32(228, 144, 255, 40);
            fog.fogColorEnd.value = new Color32(178, 209, 255, 255);
            fog.fogOne.value = 2.4f;
            fog.skyboxStrength.value = 0.1f;
            var shittyNotWorkingSun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var sunLight = Object.Instantiate(shittyNotWorkingSun.gameObject).GetComponent<Light>();
            shittyNotWorkingSun.name = "Shitty Not Working Sun";
            shittyNotWorkingSun.gameObject.SetActive(false);
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 5f;
            sunLight.shadowStrength = 0.3f;
            cgrade.colorFilter.value = new Color32(197, 233, 255, 255);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(48f, 333.0076f, 230f);

            var foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
            if (foliage)
            {
                var icicles = foliage.transform.GetChild(5);
                icicles.gameObject.SetActive(false);
            }
            DesolateFoliage();
            DesolateMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        { SiphonedForest.Vanilla.DisableSiphonedSnow(); Weather.AddRain(Intensity.Extreme, true); }


        public static void DesolateFoliage()
        {
            Assets.MeshReplaceAll([
                new(["spmGPGrass_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(43, 66, 48, 255);
                    mr.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(81, 55, 101, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(25, 87, 71, 255);
                    mr.transform.localScale = new Vector3(2f, 2f, 2f);
                }),
            ]);
        }
        public static void DesolateMaterials()
        {
            Assets.LightReplaceAll([
                new(l => !l.name.Contains("Directional Light (SUN)"), light => {
                    light.color = new Color32(53, 56, 148, 255);
                    light.intensity = 5f;
                    light.range = 120f;
                    var flickerLight = light.GetComponent<FlickerLight>();
                    if (flickerLight)
                        flickerLight.enabled = false;
                })
            ]);
            Assets.ParticleReplaceAll([new(["Fire", "HeatGas"], ps => ps.gameObject.SetActive(false))]);
            var siphonedDesolateTerrainMat = Assets.LoadRecolor("RoR2/Base/blackbeach/matBbTerrain.mat", new Color32(174, 153, 129, 255));
            siphonedDesolateTerrainMat.SetFloat("_RedChannelSmoothness", 0.5063887f);
            siphonedDesolateTerrainMat.SetFloat("_RedChannelBias", 1.2f);
            siphonedDesolateTerrainMat.SetFloat("_RedChannelSpecularExponent", 20f);
            siphonedDesolateTerrainMat.SetTexture("_RedChannelSideTex", Main.AssetBundle.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockSide.png"));
            siphonedDesolateTerrainMat.SetTexture("_RedChannelTopTex", Main.AssetBundle.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockTop.png"));
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelBias", 1.87f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSpecularStrength", 0f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSpecularExponent", 20f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSmoothnes", 0.4169469f);
            siphonedDesolateTerrainMat.SetFloat("_BlueChannelBias", 1.3f);
            siphonedDesolateTerrainMat.SetFloat("_BlueChannelSmoothness", 0.3059852f);
            siphonedDesolateTerrainMat.SetFloat("_TextureFactor", 0.06f);
            siphonedDesolateTerrainMat.SetFloat("_NormalStrength", 0.3f);
            siphonedDesolateTerrainMat.SetFloat("_Depth", 0.1f);
            siphonedDesolateTerrainMat.SetInt("_RampInfo", 5);
            siphonedDesolateTerrainMat.SetTexture("_NormalTex", Assets.Load<Texture2D>("RoR2/Base/Common/texNormalBumpyRock.jpg"));
            var siphonedDesolateTreeRingMat = Object.Instantiate(Assets.Load<Material>("RoR2/DLC1/snowyforest/matSFTreering.mat"));
            siphonedDesolateTreeRingMat.SetTexture("_SnowTex", siphonedDesolateTerrainMat.GetTexture("_GreenChannelTex"));
            Assets.MeshReplaceAll([
                new(["Terrain", "SnowPile"], mr => Assets.TryMeshReplace(mr, siphonedDesolateTerrainMat)), // siphonedDesolateTerrainMat
                new(mr => mr.gameObject.name == "SF_GiantTreesTops", mr => mr.gameObject.SetActive(false)),
                new(["Pebble", "Rock", "mdlSFCeilingSpikes"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/blackbeach/matBbBoulder.mat"))), // siphonedDesolateDetailMat
                new(["meshSnowyForestCrate", "RuinGate", "SF_Aqueduct", "meshSnowyForestFirepitRing", "meshSnowyForestFirepitJar", "mdlSFHangingLantern", "mdlSFBrokenLantern"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat", new Color32(18, 79, 40, 255)))), // siphonedDesolateDetailMat2
                new(mr => mr.gameObject.name == "meshSnowyForestFirepitFloor" || (mr.gameObject.name.Contains("meshSnowyForestPot") && mr.gameObject.name != "meshSnowyForestPotSap"), mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat", new Color32(18, 79, 40, 255)))), // siphonedDesolateDetailMat2
                new(["SF_TreeLog", "SF_TreeTrunk", "SF_GiantTrees", "SF_SurroundingTrees"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/rootjungle/matRJTree.mat", new Color32(255, 255, 255, 255)))), // siphonedDesolateDetailMat4
                new(["mdlSnowyForestTreeStump"], mr => {
                    mr.sharedMaterial = Assets.LoadRecolor("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat", new Color32(80, 162, 90, 255)); // siphonedDesolateDetailMat5
                    mr.sharedMaterials[0] = siphonedDesolateTreeRingMat; // siphonedDesolateTreeRingMat
                    mr.sharedMaterials[1] = Assets.LoadRecolor("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat", new Color32(80, 162, 90, 255)); // siphonedDesolateDetailMat5
                }),
                new(["mdlSnowyForestTreeStump"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat", new Color32(80, 162, 90, 255)))), // siphonedDesolateDetailMat6
                new(["SF_Sap", "goo"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/moon/matMoonWaterBridge.mat"))), // siphonedDesolateWaterMat
                new(mr => mr.gameObject.name == "meshSnowyForestFirepitFloor (1)", mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/moon/matMoonWaterBridge.mat"))), // siphonedDesolateWaterMat
                new(mr => mr.gameObject.name == "meshSnowyForestPotSap", mr => mr.gameObject.SetActive(false))
            ]);
        }
    }
}
