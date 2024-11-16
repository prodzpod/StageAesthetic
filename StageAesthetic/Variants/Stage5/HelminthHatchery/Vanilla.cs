using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.HelminthHatchery
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["helminthroost"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Void;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color(0.5453f, 0.4527f, 0.3988f, 0.15f);
            GameObject cameraParticles = GameObject.Find("CAMERA PARTICLES: AshParticles");
            if (cameraParticles) cameraParticles.transform.GetChild(0).gameObject.SetActive(false);
        }
    }   
}
