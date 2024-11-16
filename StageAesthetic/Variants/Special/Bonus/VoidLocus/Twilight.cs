using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Bonus.VoidLocus
{
    public class Twilight : Variant
    {
        public override string[] Stages => ["voidstage"];
        public override string Name => nameof(Twilight);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(227, 26, 150, 15);
            fog.fogColorMid.value = new Color32(80, 51, 113, 100);
            fog.fogColorEnd.value = new Color32(68, 201, 133, 50);
            fog.fogIntensity.value = 0.95f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.13f;

            cgrade.colorFilter.value = new Color32(35, 46, 99, 70);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 4f;
            sunLight.transform.eulerAngles = Vector3.zero;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newRamp = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/ColorRamps/texRampBombOrb.png").WaitForCompletion();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(70, 15, 27, 255));
            newMat.SetFloat("_AlphaBias", 0.2423056f);
            newMat.SetFloat("_AlphaBoost", 20f);
            newMat.SetTexture("_RemapTex", newRamp);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }
    }   
}
