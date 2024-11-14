using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.VerdantFalls
{
    public class Purple : Variant
    {
        public override string[] Stages => ["lakes"];
        public override string Name => nameof(Purple);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.WaterStream;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.VoidSky();
            GameObject.Find("TLTerrainOuterDistant").SetActive(false);
            GameObject tmp = GameObject.Find("Weather, Lakes");
            var sun = tmp.transform.Find("Directional Light (SUN)").gameObject;
            var probe = tmp.transform.Find("Reflection Probe").gameObject;
            sun.SetActive(true);
            probe.SetActive(true);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(222, 168, 255, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.8f;
        }
    }
}
