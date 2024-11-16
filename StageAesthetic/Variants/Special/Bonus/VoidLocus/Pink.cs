using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Bonus.VoidLocus
{
    public class Pink : Variant
    {
        public override string[] Stages => ["voidstage"];
        public override string Name => nameof(Pink);
        public override string Description => "Pink..";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogIntensity.value = 0.95f;
            fog.fogPower.value = 0.6f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.1f;

            fog.fogColorStart.value = new Color32(197, 108, 119, 10);
            fog.fogColorMid.value = new Color32(93, 29, 28, 50);
            fog.fogColorEnd.value = new Color32(0, 0, 0, 170);

            cgrade.colorFilter.value = new Color32(117, 69, 90, 255);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(255, 200, 189, 255);
            sunLight.intensity = 4f;
            sunLight.transform.eulerAngles = Vector3.zero;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(66, 0, 34, 255));
            newMat.SetFloat("_AlphaBias", 0.2f);
            newMat.SetFloat("_AlphaBoost", 11.9882f);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }
    }   
}
