using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.ShatteredAbodes
{
    public class Abandoned : Variant
    {
        public override string[] Stages => ["village"];
        public override string Name => nameof(Abandoned);
        public override string Description => "Scorching Desert.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Assets.TryDestroy("CAMERA PARTICLES: RainParticles (1)");
            Assets.TryDestroy("LVCameraParticlesGrass");
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            RampFog rampFog = Assets.GooLakeProfile.GetSetting<RampFog>();
            fog.fogColorStart.value = new Color(0.49f, 0.363f, 0.374f, 0.1f);
            fog.fogColorMid.value = new Color(0.58f, 0.486f, 0.331f, 0.25f);
            fog.fogColorEnd.value = new Color32(214, 144, 123, 128);
            fog.fogZero.value = rampFog.fogZero.value;
            fog.fogIntensity.value = rampFog.fogIntensity.value;
            fog.fogPower.value = rampFog.fogPower.value;
            fog.fogOne.value = rampFog.fogOne.value;
            fog.skyboxStrength.value = 0.02f;
            // sun.transform.eulerAngles = new Vector3(45f, 200f, 0f);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color(1f, 0.65f, 0.5f, 1f);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 0.7f;
            Common.AbodesMaterials(
                Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(230, 223, 174, 125)), 
                Assets.Load<Material>("RoR2/Base/goolake/matGoolake.mat"),
                Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat"),
                Assets.LoadRecolor("RoR2/Base/goolake/matGoolake.mat", new Color32(176, 153, 57, 255))
            );
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Extreme);
    }
}
