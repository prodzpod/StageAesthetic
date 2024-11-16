using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Endings.Commencement
{
    public class Crimson : Variant
    {
        public override string[] Stages => ["moon2"];
        public override string Name => nameof(Crimson);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Rain;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = -0.1f;
            fog.fogOne.value = 0.7f;
            fog.fogColorStart.value = new Color32(0, 0, 0, 0);
            fog.fogColorMid.value = new Color32(156, 31, 33, 50);
            fog.fogColorEnd.value = new Color32(93, 0, 18, 255);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.GetComponent<Light>().intensity = 0.15f;
            newSun.color = new Color32(255, 9, 0, 255);
            newSun.intensity = 0.4f;
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.79f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.1086f, 0.15f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.1091f, 0.1226f, 1);
            ChangeFlames(Assets.Load<Material>("RoR2/Base/Common/VFX/matFireStaticLarge.mat"), new Color32(156, 31, 33, 255));
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Heavy);
        public static void ChangeFlames(Material flameMat, Color flameColor)
        {
            Assets.MeshReplaceAll([
                new(["BazaarLight", "mdlLunarCoolingBowlLarge"], mr => {
                    if (!mr.sharedMaterial) return;
                    ParticleSystemRenderer fire = mr.gameObject.transform.GetComponentInChildren<ParticleSystemRenderer>();
                    if (!fire) return;
                    fire.sharedMaterial = flameMat;
                    Light fireLight = mr.gameObject.transform.GetComponentInChildren<Light>();
                    if (fireLight)
                        fireLight.color = flameColor;
                })
            ]);
        }
    }   
}
