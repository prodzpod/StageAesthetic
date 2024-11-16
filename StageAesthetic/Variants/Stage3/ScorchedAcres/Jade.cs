using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Jade : Variant
    {
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Jade);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop); 
            fog.fogColorStart.value = new Color32(70, 90, 84, 0);
            fog.fogColorMid.value = new Color32(74, 99, 105, 100);
            fog.fogColorEnd.value = new Color32(77, 113, 85, 150);
            fog.skyboxStrength.value = 0;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(152, 255, 255, 255);
            sunLight.intensity = 0.75f;
            sunTransform.eulerAngles = new Vector3(75, 115, 180);
            lightBase.Find("CameraRelative").Find("SunHolder").gameObject.SetActive(false);
            lightBase.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            var eclipse = Night.FindEclipseGameObject(GameObject.Find("Weather, Wispgraveyard").scene);
            if (eclipse != null)
            {
                eclipse.SetActive(true);
                eclipse.transform.GetChild(1).gameObject.SetActive(false); // lighting
                eclipse.transform.GetChild(2).gameObject.SetActive(false); // post-processing
                eclipse.transform.GetChild(4).GetChild(1).gameObject.SetActive(false); // dust
            }
        }
    }   
}
