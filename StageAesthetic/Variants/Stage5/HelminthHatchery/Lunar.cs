using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.HelminthHatchery
{
    public class Lunar : Variant
    {
        public override string[] Stages => ["helminthroost"];
        public override string Name => nameof(Lunar);
        public override string Description => "Bleu and ashy terrain.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            GameObject cameraParticles = GameObject.Find("CAMERA PARTICLES: AshParticles");
            if (cameraParticles) cameraParticles.transform.GetChild(0).gameObject.SetActive(false);
            fog.fogColorEnd.value = new Color(0.5f, 0.5f, 0.6f, 1);
            fog.fogColorMid.value = new Color(0f, 0.42f, 0.57f, 0.4471f);
            fog.fogColorStart.value = new Color(0f, 0.45f, 0.47f, 0.1f);
            var terrainMat = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/helminthroost/Assets/matHRTerrain.mat"));
            var moonTerrainMat = Assets.Load<Material>("RoR2/Base/moon/matMoonTerrain.mat");
            Texture2D blueTex = moonTerrainMat.GetTexture("_BlueChannelTex") as Texture2D;
            Texture2D greenTex = moonTerrainMat.GetTexture("_GreenChannelTex") as Texture2D;
            terrainMat.SetTexture("_GreenChannelTex", blueTex);
            terrainMat.SetTexture("_BlueChannelTex", greenTex);
            // HRFireStatic
            Assets.ReplaceAll<ParticleSystemRenderer>([new(["HRFireStatic"], psr => {
                if (!psr.sharedMaterial) return;
                psr.sharedMaterial = Assets.Load<Material>("RoR2/Base/Common/VFX/matFireStaticBlueLarge.mat");
                if (psr.transform.GetChild(0)) psr.transform.GetChild(0).GetComponent<Light>().color = new Color(0f, 0.4704f, 0.9608f, 1);
            })]);
            Assets.MeshReplaceAll([
                new(["HRSection", "HRGroundCover"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/moon/matMoonBridge.mat"))),
                new(["HRStalagmitesCombined"], mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(mr => mr.gameObject.name.Contains("HRTerrain") && !mr.gameObject.name.Contains("Lava"), mr => Assets.TryMeshReplace(mr, terrainMat)),
                new(["HRWorm"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/moon/matMoonBaseStandTriplanar.mat"))),
                new(["HRRock", "Volcanoid", "HRObisidian"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/moon/matMoonBoulder.mat"))),
            ]);
        }
    }   
}
