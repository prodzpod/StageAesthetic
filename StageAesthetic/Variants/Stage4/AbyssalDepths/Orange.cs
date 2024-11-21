using StageAesthetic.Variants.Stage3.SulfurPools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Orange : Variant
    {
        public override float PreLoopWeightDefault => 0;
        public override float LoopWeightDefault => 0;
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Orange);
        public override string Description => "Pink with some orange and blue.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.DaySky();
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1f;
            sunLight.transform.eulerAngles = new Vector3(70f, 19.64314f, 9.985f);
            sunLight.shadowStrength = 0.75f;

            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(85, 57, 91, 33);
            caveFog.fogColorMid.value = new Color32(90, 55, 97, 100);
            caveFog.fogColorEnd.value = new Color32(135, 76, 149, 150);

            var abyssalGoldTerrainMat = Object.Instantiate(Assets.Load<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat"));
            abyssalGoldTerrainMat.SetTexture("_GreenChannelTex", Assets.Load<Texture2D>("RoR2/DLC1/itskymeadow/texSMGrassTerrainInfiniteTower.png"));
            Common.SimMaterials(
                abyssalGoldTerrainMat, 
                Assets.Load<Material>("RoR2/Base/dampcavesimple/matDCBoulder.mat"), 
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainWallsInfiniteTower.mat"), 
                Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"), 
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat")
            );
            Common.LightChange(new Color(0.64f, 0.343f, 0.924f, 1), new Color(0.981f, 0.521f, 0.065f), new Color(0.598f, 0.117f, 0.355f));
        }
    }   
}
