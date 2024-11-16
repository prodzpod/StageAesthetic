using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.SulfurPools
{
    public class Hell : Variant
    {
        public override string[] Stages => ["sulfurpools"];
        public override string Name => nameof(Hell);
        public override string Description => "Texture swap to Red Aphelian Sanctuary.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            // cgrade.SetAllOverridesTo(true);
            // cgrade.colorFilter.value = new Color32(181, 178, 219, 255);
            fog.skyboxStrength.value = 0.0f;
            fog.fogColorStart.value = new Color32(89, 56, 138, 20);
            fog.fogColorMid.value = new Color32(63, 71, 87, 60);
            fog.fogColorEnd.value = new Color32(48, 57, 76, 189);
            fog.skyboxStrength.value = -0.2f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 1f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.07f;

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(204, 130, 139, 255);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 0.6f;
            var fogg = GameObject.Find("mdlSPTerrain");
            fogg.transform.GetChild(3).gameObject.SetActive(false);
            fogg.transform.GetChild(5).gameObject.SetActive(false);
            fogg.transform.GetChild(12).gameObject.SetActive(false);
            fogg.transform.GetChild(14).gameObject.SetActive(false);
            var goofyAhh = GameObject.Find("PP + Amb").GetComponent<PostProcessVolume>().sharedProfile;
            try { goofyAhh.RemoveSettings<DepthOfField>(); } catch { }
            try { goofyAhh.RemoveSettings<Bloom>(); } catch { }
            try { goofyAhh.RemoveSettings<Vignette>(); } catch { }
            var fuckYou = GameObject.Find("HOLDER: Skybox");
            fuckYou.transform.GetChild(10).gameObject.SetActive(false);
            fuckYou.transform.GetChild(11).gameObject.SetActive(false);
            fuckYou.transform.GetChild(12).gameObject.SetActive(false);
            fuckYou.transform.GetChild(13).gameObject.SetActive(false);
            GameObject.Find("SPCavePP").SetActive(false);
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(9).gameObject.SetActive(false);
            GameObject.Find("HOLDER: SulfurPods").SetActive(false);
            string[] targets = ["SulfurPodBody(Clone)"];
            foreach (string name in targets)
            {
                GameObject go = GameObject.Find(name);
                // annihilate all pods
                if (go) go.SetActive(false);
            }
            //AddRain(RainType.Typhoon, true);
            Common.VanillaWater();
            HellMaterials();
        }
        public struct J(Transform t, int[] indices, Material mat)
        {
            public Transform t = t;
            public int[] indices = indices;
            public Material mat = mat;
        }
        public static void HellMaterials()
        {
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            var sphere = GameObject.Find("mdlSPSphere").transform;
            // var terrainMat = GameObject.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat").WaitForCompletion());
            J[] js = [
                new(terrain, [0], Assets.Load<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat")), 
                new(terrain, [2, 4, 8, 11, 13], Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat", new Color32(0, 0, 0, 204))), 
                new(terrain, [7, 10], Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat")), 
                new(sphere, [7, 11], Assets.LoadRecolor("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat", new Color32(0, 0, 0, 204))), 
                new(sphere, [0, 3, 4, 5, 6, 12, 13, 14], Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"))
            ];
            foreach (var j in js) foreach (var i in j.indices)
            {
                var go = j.t.GetChild(i).gameObject;
                if (!go || !go.GetComponent<MeshRenderer>()) continue;
                go.GetComponent<MeshRenderer>().sharedMaterial = j.mat;
            }
            // this shit unj hug hag
        }
    }   
}
