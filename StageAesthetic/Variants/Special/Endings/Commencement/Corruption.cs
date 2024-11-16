using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Endings.Commencement
{
    public class Corruption : Variant
    {
        public override string[] Stages => ["moon2"];
        public override string Name => nameof(Corruption);
        public override string Description => "Purple.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.3f;
            fog.fogColorStart.value = new Color32(77, 23, 107, 45);
            fog.fogColorMid.value = new Color32(104, 44, 107, 105);
            fog.fogColorEnd.value = new Color32(75, 0, 75, 255);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.GetComponent<Light>().intensity = 0.22f;
            newSun.color = new Color32(53, 94, 225, 255);
            newSun.intensity = 0.5f;
            newSun.shadowStrength = 1f;
            newSun.transform.eulerAngles = new Vector3(30.5f, 0f, 0f);
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.5f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.1086f, 0.2138f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.1091f, 0.2012f, 1);
            Common.ChangeFlames(Assets.Load<Material>("RoR2/DLC1/voidoutro/matFireStaticVoidOutroEyePurple.mat"), new Color32(156, 31, 33, 255));
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Extreme);
    }   
}
