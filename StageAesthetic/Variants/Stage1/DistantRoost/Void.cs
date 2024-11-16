using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Void : Variant
    {
        public override string[] Stages => ["blackbeach"];
        public override string Name => nameof(Void);
        public override string Description => "Texture swap to Purple Void Fields.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.VoidSky();
            Assets.TryDestroy("mdlBBCliffLarge1 (5)");
            Assets.TryDestroy("mdlBBCliffLarge1 (6)");
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 9;
                            light.range = 70;
                            light.color = new Color32(109, 58, 119, 140);
                        }
                    }
                }
            }
            GameObject.Find("HOLDER: Grass").SetActive(false);
            GameObject.Find("FOLIAGE").SetActive(false);
            var s = GameObject.Find("SKYBOX").transform;
            s.GetChild(6).gameObject.SetActive(false);
            s.GetChild(11).gameObject.SetActive(false);
            Common.VanillaFoliage();
            VoidMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
             => Weather.AddSnow(Intensity.Mild, -10f);
        public static void VoidMaterials()
        {
            var distantRoostVoidTerrainMat = Assets.LoadRecolor("RoR2/Base/arena/matArenaTerrain.mat", new(3, 0, 255, 255));
            distantRoostVoidTerrainMat.SetFloat("_NormalStrength", 0.1f);
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = distantRoostVoidTerrainMat;
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = distantRoostVoidTerrainMat;

            Assets.MeshReplaceAll([
                new(["Boulder", "Rock", "Step", "Tile", "mdlGeyser", "Pebble", "Detail"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat"))),
                new(["Bowl", "Marker", "RuinPillar"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat"))),
                new(["DistantPillar", "Cliff", "ClosePillar"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/arena/matArenaTerrainVerySnowy.mat", new(188, 162, 162, 255)))),
                new(["Decal", "spmBbFern2"], mr => mr.gameObject.SetActive(false)),
                new(["GlowyBall"], mr => mr.sharedMaterial.color = new Color32(109, 58, 119, 140))
            ]);
            GameObject.Find("SKYBOX").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = Assets.LoadRecolor("RoR2/Base/goldshores/matGSWater.mat", new(0, 14, 255, 255));
        }
    }
}
