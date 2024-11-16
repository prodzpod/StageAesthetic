using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Twilight : Variant
    {
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Twilight);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.SingularitySky();
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            lightBase.GetChild(2).gameObject.SetActive(false);
            lightBase.Find("Directional Light (SUN)").gameObject.SetActive(false);
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.gameObject.SetActive(false);
        }
    }   
}
