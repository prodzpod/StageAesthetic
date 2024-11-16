using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Bonus.VoidLocus
{
    public class Blue : Variant
    {
        public override string[] Stages => ["voidstage"];
        public override string Name => nameof(Blue);
        public override string Description => "Dark Blue..";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(81, 81, 142, 20);
            fog.fogColorMid.value = new Color32(76, 96, 150, 35);
            fog.fogColorEnd.value = new Color32(55, 46, 117, 111);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.3f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.182f;

            cgrade.colorFilter.value = new Color32(102, 115, 176, 255);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(30, 28, 99, 255);
            sunLight.intensity = 2.6f;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(2, 0, 255, 255));
            newMat.SetFloat("_AlphaBias", 0.2f);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }
    }   
}
