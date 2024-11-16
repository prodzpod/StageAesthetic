using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.SulfurPools
{
    public class Void : Variant
    {
        public override string[] Stages => ["sulfurpools"];
        public override string Name => nameof(Void);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var sunTransform = GameObject.Find("Directional Light (SUN)");
            sunTransform.gameObject.SetActive(false);
            var goofyAhh = GameObject.Find("PP + Amb");
            if (goofyAhh)
                goofyAhh.gameObject.SetActive(false);
            Skybox.VoidSky();

            var fogg = GameObject.Find("mdlSPTerrain");
            foreach (var i in new int[] { 3, 5, 12, 14 }) fogg.transform.GetChild(i).gameObject.SetActive(false);
            var fuckYou = GameObject.Find("HOLDER: Skybox");
            foreach (var i in new int[] { 10, 11, 12, 13 }) fuckYou.transform.GetChild(i).gameObject.SetActive(false);
            GameObject.Find("SPCavePP").SetActive(false);
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/DLC1/sulfurpools/matSPWaterGreen.mat");
            Assets.MeshReplaceAll([
                new(["Terrain", "Mountain"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/arena/matArenaTerrain.mat", new Color32(255, 255, 255, 96)))),
                new(["meshSPSphere", "SPHeatVent", "Crystal", "Boulder", "mdlGeyser", "Pebble", "Spikes", "Dome", "Cave", "Eel"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat"))),
                new(["Moss", "SPCoral", "HeatGas", "Stinky", "Grass", "Vine"], mr => mr.gameObject.SetActive(false)),
            ]); 
            VoidWater();
        }
        public static void VoidWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            if (waterBlue && waterGreen && waterRed && waterYellow)
            {
                waterBlue.color = new Color32(255, 219, 0, 184);
                waterGreen.color = new Color32(0, 255, 229, 255);
                waterRed.color = new Color32(178, 38, 171, 200);
                waterYellow.color = new Color32(0, 184, 255, 212);
            }
        }
    }   
}
