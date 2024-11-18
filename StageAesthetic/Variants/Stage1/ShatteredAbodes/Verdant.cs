using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.ShatteredAbodes
{
    public class Verdant : Variant
    {
        public override string[] Stages => ["village", "villagenight"];
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
            fog.fogIntensity.value = 0.4f;
            fog.fogPower.value = 0.5f;
            fog.skyboxStrength.value = 0f;

            if (scenename == "village")
            {
                GameObject rainParticles = GameObject.Find("CAMERA PARTICLES: RainParticles (1)");
                rainParticles.SetActive(false);
            }
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            sun.transform.eulerAngles = new Vector3(60f, 65f, 210f);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.7f;
            // 30.512 64.27 209.701
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            var verdantTerrainMat = Object.Instantiate(Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLTerrainStone.mat"));
            verdantTerrainMat.SetFloat("_RedChannelBias", 0.4f);
            verdantTerrainMat.SetFloat("_GreenChannelBias", -0.2f);
            verdantTerrainMat.SetFloat("_BlueChannelBias", -0.03f);
            Common.AbodesMaterials(verdantTerrainMat,
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLRocks.mat"),
                Assets.Load<Material>("RoR2/DLC2/lakes/Assets/matTLShip.mat"),
                Assets.LoadRecolor("RoR2/DLC2/lakes/Assets/matTLGVine.mat", new Color32(255, 255, 255, 255))
            );
        }
    }
}
