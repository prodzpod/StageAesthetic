using StageAesthetic.Variants.Stage3.SulfurPools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Blue : Variant
    {
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Blue);
        public override string Description => "Orange-ish and pink-ish";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(48, 102, 102, 81); // A cyan-ish color
            fog.fogColorMid.value = new Color32(61, 87, 94, 93);    // A darker cyan
            fog.fogColorEnd.value = new Color32(32, 142, 121, 200); // A bluish-green, which complements red
            fog.fogOne.value = 0.3f;
            fog.fogIntensity.value = 0.65f;
            fog.fogZero.value = -0.02f;

            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(229, 214, 255, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.6f;
            sunLight.transform.eulerAngles = new Vector3(65f, 222.6395f, 202.9964f);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(78, 55, 80, 60);
            caveFog.fogColorMid.value = new Color32(64, 75, 94, 144);
            caveFog.fogColorEnd.value = new Color32(60, 73, 88, 205);
            Common.SimMaterials(
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainWallsInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianRuinsHeavyInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat")
            );
            // Lighting: Magenta coral, orange otherwise
            Common.LightChange(new Color32(30, 209, 27, 255), new Color(0.981f, 0.521f, 0.065f), new Color(0.718f, 0, 0.515f));
        }
    }   
}
