using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 3f;
            sunLight.transform.localEulerAngles = new Vector3(35, 15, 351);
            sunLight.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            sunLight.shadowStrength = 0.6f;
        }
    }   
}
