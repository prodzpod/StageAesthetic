﻿using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.ReformedAltar
{
    public class Verdant: Variant
    {
        public override string[] Stages => ["lemuriantemple"];
        public override string Name => nameof(Verdant);
        public override string Description => "Sunny and bright green.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;

            GameObject sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
            Assets.MeshReplaceAll([new(["GrassSmall"], mr => mr.sharedMaterial.color = new Color(0.3786f, 0.6321f, 0.5703f, 1))]);

            Material terrainMat = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLTerrainStone.mat"));
            terrainMat.SetFloat("_RedChannelBias", 0.4f);
            terrainMat.SetFloat("_GreenChannelBias", -0.2f);
            terrainMat.SetFloat("_BlueChannelBias", -0.03f);
            Material detailMat2 = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/lemuriantemple/Assets/matLTFloor.mat"));
            Material detailMat22 = Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLShip.mat");
            detailMat2.SetTexture("_GreenChannelTex", detailMat22.GetTexture("_SnowTex"));
            detailMat2.SetTexture("_BlueChannelTex", detailMat22.GetTexture("_MainTex"));
            detailMat2.SetTexture("_RedChannelTopTex", detailMat22.GetTexture("_DirtTex"));
            detailMat2.SetTexture("_RedChannelSideTex", detailMat22.GetTexture("_DirtTex"));
            Common.AltarMaterials(
                terrainMat,
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLRocks.mat"),
                detailMat2,
                Assets.LoadRecolor("RoR2/DLC2/lakes/Assets/matTLGVine.mat", new(255, 255, 255, 255)),
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLGrassSparseBlue.mat")
            );
        }
    }   
}
