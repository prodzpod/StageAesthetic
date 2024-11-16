using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Sunset : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Sunset);
        public override string Description => "Orange sun over greenery.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 150);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1.1f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;
            Assets.TryDestroy("Weather, Golemplains");
            Common.VanillaFoliage();
        }
    }
}
