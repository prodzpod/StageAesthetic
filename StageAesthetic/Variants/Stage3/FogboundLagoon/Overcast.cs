using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.FogboundLagoon
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["FBLScene"];
        public override string Name => nameof(Overcast);
        public override string Description => "Very foggy with rain.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(140, 117, 150, 37);
            fog.fogColorMid.value = new Color32(84, 89, 117, 50);
            fog.fogColorEnd.value = new Color32(74, 87, 91, 255);
            fog.skyboxStrength.value = 0f;
            fog.fogZero.value = -0.12f;
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(136, 157, 162, 255);
            var wc = GameObject.Find("HOLDER: Weather");
            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            waterPP.weight = 1f;
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            var cg2 = waterPP.profile.GetSetting<ColorGrading>();
            var vn = waterPP.profile.GetSetting<Vignette>();
            vn.SetAllOverridesTo(true);
            vn.intensity.value = 0.2f;
            vn.color.value = new Color32(89, 86, 138, 255);
            cg2.SetAllOverridesTo(true);
            cg2.contrast.value = 20f;
            cg2.colorFilter.value = new Color32(83, 197, 153, 255);
            cg2.tint.value = 30f;
            fog2.fogColorStart.value = new Color32(249, 193, 255, 62);
            fog2.fogColorMid.value = new Color32(67, 89, 89, 13);
            fog2.fogColorEnd.value = new Color32(53, 62, 51, 255);
            fog2.fogOne.value = 0.06f;

            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Heavy);
    }   
}
