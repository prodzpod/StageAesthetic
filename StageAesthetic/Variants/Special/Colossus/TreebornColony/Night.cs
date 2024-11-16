using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.TreebornColony
{
    public class Night : Variant
    {
        public override string[] Stages => ["habitat"];
        public override string Name => nameof(Night);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();
            GameObject.Find("meshBHFog").SetActive(false);
            GameObject.Find("Weather, Habitat").SetActive(false);
        }
    }   
}
