using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.FogboundLagoon
{
    public class Clear: Variant
    {
        public override string[] Stages => ["FBLScene"];
        public override string Name => nameof(Clear);
        public override string Description => "Yellow sun over greenery with less fog.";
        public override SoundType Ambience => SoundType.WaterStream;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Weather.PlaySound(SoundType.DayNature);
            fog.fogPower.value = 1f;
            fog.fogColorStart.value = new Color32(130, 126, 27, 5);
            fog.fogColorMid.value = new Color32(84, 116, 117, 61);
            fog.fogColorEnd.value = new Color32(95, 135, 181, 255);
            fog.fogOne.value = 0.7f;
            fog.fogZero.value = -0.04f;
            var sun = GameObject.Find("HOLDER: Lights, FX, Wind").transform.GetChild(0);
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            var newSun = sun2.GetComponent<Light>();
            newSun.intensity = 1f;
            newSun.shadowStrength = 0.7f;

            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            fog2.fogColorStart.value = new Color32(130, 126, 27, 0);
            fog2.fogColorMid.value = new Color32(77, 142, 136, 61);
            fog2.fogColorEnd.value = new Color32(78, 110, 123, 230);
            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = 0f;
            fog2.fogOne.value = 0.2f;
            var cg = waterPP.profile.GetSetting<ColorGrading>();
            cg.colorFilter.value = new Color32(191, 255, 244, 255);
        }
    }   
}
