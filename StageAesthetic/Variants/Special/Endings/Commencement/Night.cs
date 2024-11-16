using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Endings.Commencement
{
    public class Night : Variant
    {
        public override string[] Stages => ["moon2"];
        public override string Name => nameof(Night);
        public override string Description => "Very dark with a great view.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();

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
            bruh2.overrideAmbientColor = new Color(0.0138f, 0.086f, 0.015f, 1);
            bruh2.overrideDirectionalColor = new Color(0.012f, 0.091f, 0.0226f, 1);
        }
    }   
}
