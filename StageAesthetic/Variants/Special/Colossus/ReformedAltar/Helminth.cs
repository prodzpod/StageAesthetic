using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.ReformedAltar
{
    public class Helminth : Variant
    {
        public override string[] Stages => ["lemuriantemple"];
        public override string Name => nameof(Helminth);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorEnd.value = new Color(0.3208f, 0.1234f, 0.1044f, 1f);
            fog.fogColorMid.value = new Color(0.5176f, 0.3338f, 0.2706f, 0.4471f);
            fog.fogColorStart.value = new Color(0.7453f, 0.3527f, 0.2988f, 0f);

            GameObject.Find("Leaves").SetActive(false);
            GameObject.Find("FallenLeaf").SetActive(false);
            GameObject.Find("LTVineHanging").SetActive(false);
            GameObject.Find("LTVineHangingB").SetActive(false);

            GameObject sun = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color(0.5647f, 0.8706f, 0.8863f, 1);

            Assets.MeshReplaceAll([
                new(["LTCrystals"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRObsidian.mat"))),
                new(["GrassSmall"], mr => mr.sharedMaterial.color = new Color(1f, 0.7468f, 0.5868f, 1))
            ]);
            var helminthTerrainMat = Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRTerrain.mat");
            Material terrainMat = Object.Instantiate(helminthTerrainMat);
            terrainMat.SetTexture("_GreenChannelTex", helminthTerrainMat.GetTexture("_BlueChannelTex"));
            terrainMat.SetTexture("_BlueChannelTex", helminthTerrainMat.GetTexture("_GreenChannelTex"));
            Material detailMat2 = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRWalls.mat"));
            detailMat2.shaderKeywords = ["DOUBLESAMPLE", "MICROFACET_SNOW", "USE_ALPHA_AS_MASK", "USE_VERTEX_COLORS", "USE_VERTICAL_BIAS", "BINARYBLEND"];
            Verdant.AltarMaterials(
                terrainMat,
                Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRRocks.mat"), 
                detailMat2,
                Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRTerrainLava.mat"), 
                Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRFireBlossom.mat")
            );
        }
    }   
}
