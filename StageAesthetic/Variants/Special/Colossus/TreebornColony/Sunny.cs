using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.TreebornColony
{
    public class Sunny : Variant
    {
        public override string[] Stages => ["habitat"];
        public override string Name => nameof(Sunny);
        public override string Description => "Sunny, blue sky, and changes sun's angle.";
        public override SoundType Ambience => SoundType.DayNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            SetAmbientLight amb = volume.GetComponent<SetAmbientLight>();
            amb.ambientSkyColor = new Color(0.88078f, 0.8431f, 0.5373f, 1);
            amb.ApplyLighting();
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            // 0.6078 0.6431 0.5373 1
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;
            fog.fogPower.value = 1f;
            GameObject.Find("meshBHFog").SetActive(false);
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            sun.transform.eulerAngles = new Vector3(90, 0, 0); // 64.0354 107.4961 67.9778
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
        }
    }   
}
