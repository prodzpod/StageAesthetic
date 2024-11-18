using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.TreebornColony
{
    public class Night : Variant
    {
        public override string[] Stages => ["habitat", "habitatfall"];
        public override string Name => nameof(Night);
        public override string Description => "Dark and blue with a starry sky.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();
            GameObject.Find("meshBHFog").SetActive(false);
            Assets.TryDestroy("Weather, Habitat");
            Assets.TryDestroy("Weather, HabitatFall");
        }
    }   
}
