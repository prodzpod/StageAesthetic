using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TerrainUtils;

namespace StageAesthetic.Variants.Stage4.SirensCall
{
    public class Aphelian : Variant
    {
        public override string[] Stages => ["shipgraveyard"];
        public override string Name => nameof(Aphelian);
        public override string Description => "Texture swap to Blue/Yellow/Orange Aphelian Sanctuary.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            GameObject.Find("Directional Light (SUN)").SetActive(false);

            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(122, 69, 56, 5);
            fog.fogColorMid.value = new Color32(122, 69, 56, 35);
            fog.fogColorEnd.value = new Color32(91, 52, 42, 230);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.25f;
            // cgrade.colorFilter.value = new Color32(7, 0, 140, 10);
            // cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0f;
            fog.fogOne.value = 0.085f;

            Assets.MeshReplaceAll([
                new(["Ship"], mr => {
                    var light = mr.gameObject.AddComponent<Light>();
                    light.color = new Color32(255, 235, 223, 255);
                    light.range = 40f;
                    light.intensity = 2.7f;
                }),
                new(["Grass"], mr => {
                    if (mr.sharedMaterial) return;
                    mr.sharedMaterial.color = new Color32(83, 99, 103, 220);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(176, 124, 59, 106);
                }),
                new(["DanglingMoss"], mr => { if (mr.sharedMaterial) mr.sharedMaterial.color = new Color32(232, 193, 75, 139); }),
                new(["Hologram"], mr => {
                    var light = mr.gameObject.AddComponent<Light>();
                    light.color = new Color32(251, 181, 56, 255);
                    light.range = 40f;
                    light.intensity = 15f;
                })
            ]);
            AphelianMaterials();
        }

        public static void AphelianMaterials()
        {
            Assets.MeshReplaceAll([
                new(["Spikes", "Stalactite", "Stalagmite", "Level Wall", "Mesh"], mr => {
                    var parent = mr.transform.parent.gameObject;
                    if (!parent) return;
                    if (parent.name.Contains("Cave") || parent.name.Contains("Terrain") || parent.name.Contains("Stalagmite"))
                        Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat", new Color32(138, 176, 167, 255)));
                }),
                new(["Terrain", "Cave", "Floor"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat", new Color32(138, 176, 167, 255)))),
                new(["Ship"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/Common/TrimSheets/matTrimSheetAlien1BossEmissionDirty.mat", new Color32(252, 154, 72, 235)))),
                new(["Rock", "Boulder"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/DLC1/ancientloft/matAncientLoft_StoneSurface.mat", new Color32(178, 127, 68, 159)))),
                new(["Hologram"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat"))),
            ]);
        }
    }   
}
