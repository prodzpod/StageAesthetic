using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.SirensCall
{
    public class Night : Variant
    {
        public override string[] Stages => ["shipgraveyard"];
        public override string Name => nameof(Night);
        public override string Description => "Dark and green.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSkyNoBullshit();

            fog.fogColorStart.value = new Color32(39, 81, 107, 0);
            fog.fogColorMid.value = new Color32(15, 62, 50, 99);
            fog.fogColorEnd.value = new Color32(10, 40, 36, 255);
            cgrade.colorFilter.value = new Color32(171, 223, 227, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0.8f;
            fog.fogOne.value = 0.085f;

            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(155, 163, 227, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.5f;
            Common.ChangeGrassColor();
        }
    }   
}
