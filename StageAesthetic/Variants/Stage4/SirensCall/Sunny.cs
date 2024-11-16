using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.SirensCall
{
    public class Sunny : Variant
    {
        public override string[] Stages => ["shipgraveyard"];
        public override string Name => nameof(Sunny);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop); 
            Skybox.DaySky();
            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 239, 223, 255);
            sunLight.intensity = 1.5f;
            sunLight.shadowStrength = 0.75f;
            sunTransform.localEulerAngles = new Vector3(33, 0, 0);
            Night.ChangeGrassColor();
        }
    }   
}
