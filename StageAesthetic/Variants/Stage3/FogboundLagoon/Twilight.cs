using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.FogboundLagoon
{
    public class Twilight: Variant
    {
        public override string[] Stages => ["FBLScene"];
        public override string Name => nameof(Twilight);
        public override string Description => "Purple and orange fog.";
        public override SoundType Ambience => SoundType.WaterStream;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Weather.PlaySound(SoundType.DayNature);
            fog.fogIntensity.value = 0.8f;
            fog.fogPower.value = 0.43f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.4f;
            fog.fogColorStart.value = new Color32(120, 113, 158, 0);
            fog.fogColorMid.value = new Color32(128, 102, 72, 57);
            fog.fogColorEnd.value = new Color32(85, 74, 91, 233);
            fog.skyboxStrength.value = 0.02f;
            var sun = GameObject.Find("HOLDER: Lights, FX, Wind").transform.GetChild(0);
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            var newSun = sun2.GetComponent<Light>();
            newSun.intensity = 0.7f;
            newSun.color = new Color32(255, 213, 250, 255);
            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            var cg = waterPP.profile.GetSetting<ColorGrading>();
            fog2.fogColorStart.value = new Color32(255, 193, 238, 21);
            fog2.fogColorMid.value = new Color32(100, 84, 107, 36);
            fog2.fogColorEnd.value = new Color32(91, 65, 86, 255);
        }
    }   
}
