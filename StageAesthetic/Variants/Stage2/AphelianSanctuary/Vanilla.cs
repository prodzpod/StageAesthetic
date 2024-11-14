using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2.AphelianSanctuary
{
    public class Vanilla : Variant
    {
        public override string[] Stages => ["ancientloft"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            GameObject sun = GameObject.Find("AL_Sun");
            if (sun)
            {
                sun.SetActive(false);
                GameObject newSun = Object.Instantiate(Skybox.sun, sun.transform.parent);
                newSun.transform.localPosition = new Vector3(-897.0126f, 350f, 209.9904f);
                newSun.transform.eulerAngles = new Vector3(275f, 90f, 90f);
                newSun.GetComponent<MeshRenderer>().sharedMaterial = Skybox.sunMat;
            }
        }
    }
}
