using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Sunny : Variant
    {
        public override float PreLoopWeightDefault => 0;
        public override float LoopWeightDefault => 0;
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Sunny);
        public override string Description => "Brings back the unreleased Scorched Acres' look.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(128, 121, 99, 13);
            fog.fogColorMid.value = new Color32(106, 141, 154, 130);
            fog.fogColorEnd.value = new Color32(104, 150, 199, 255);
            fog.fogZero.value = -0.058f;
            fog.fogPower.value = 1.2f;
            fog.fogIntensity.value = 0.937f;
            cgrade.colorFilter.value = new Color32(240, 213, 248, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0f;

            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 225, 181, 255);
            sunLight.intensity = 1.6f;
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.transform.position = new Vector3(-250, 90, -199);
            sunHolder.localEulerAngles = new Vector3(20f, 57.2f, 0f);
            var sunBase = sunHolder.Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            Transform[] quadCount = [sunBase.GetChild(0), sunBase.GetChild(1)];
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }
    }   
}
