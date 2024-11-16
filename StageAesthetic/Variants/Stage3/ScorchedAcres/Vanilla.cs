using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(36, 0, 0);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            Transform[] quadCount = [sunBase.GetChild(0), sunBase.GetChild(1)];
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }
    }   
}
