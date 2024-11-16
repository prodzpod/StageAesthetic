using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Endings.Commencement
{
    public class Gray : Variant
    {
        public override string[] Stages => ["moon2"];
        public override string Name => nameof(Gray);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 1f;
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.1f;
            fog.fogColorStart.value = new Color32(120, 120, 120, 50);
            fog.fogColorMid.value = new Color32(100, 100, 100, 100);
            fog.fogColorEnd.value = new Color32(90, 90, 90, 200);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            newSun.name = "Directional Light (SUN)";
            newSun.color = new Color32(53, 94, 225, 255);
            newSun.intensity = 0.5f;
            newSun.shadowStrength = 1f;
            newSun.transform.eulerAngles = new Vector3(49.10302f, 313.86f, 308.234f);
            newSun.transform.localPosition = new Vector3(-26f, 138f, 335f);
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.28f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            // 0.1138 0.1086 0.15 1
            // 0.1012 0.1091 0.1226 1
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.2086f, 0.25f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.2091f, 0.2226f, 1);
        }
    }   
}
