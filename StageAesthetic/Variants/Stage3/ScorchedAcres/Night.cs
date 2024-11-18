using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public class Night : Variant
    {
        public override string[] Stages => ["wispgraveyard"];
        public override string Name => nameof(Night);
        public override string Description => "Dark purple with stars!";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(51, 55, 83, 30);
            fog.fogColorMid.value = new Color32(30, 29, 66, 130);
            fog.fogColorEnd.value = new Color32(43, 36, 60, 255);
            fog.skyboxStrength.value = 0.03f;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            lightBase.GetChild(1).GetChild(0).gameObject.SetActive(false); // embers
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(173, 175, 245, 255);
            sunLight.intensity = 2.3f;
            sunLight.shadowStrength = 0.35f;
            sunLight.shadowBias = 0.05f;
            lightBase.Find("CameraRelative").Find("SunHolder").gameObject.SetActive(false);
            var eclipse = Common.FindEclipseGameObject(GameObject.Find("Weather, Wispgraveyard").scene);
            if (eclipse != null)
            {
                eclipse.SetActive(true);
                eclipse.transform.GetChild(1).gameObject.SetActive(false); // lighting
                eclipse.transform.GetChild(2).gameObject.SetActive(false); // post-processing
                eclipse.transform.GetChild(4).gameObject.SetActive(false); // weather
                eclipse.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                eclipse.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);

                var sphere = eclipse.transform.GetChild(3).GetChild(2).gameObject;

                var moonPosition = sphere.transform.position;
                moonPosition.y = 263;

                sphere.transform.localScale = new Vector3(8, 8, 8);

                var meshRenderer = sphere.GetComponent<MeshRenderer>();

                var coolerMat = new Material(Assets.Load<Material>("RoR2/Base/eclipseworld/matEclipseMoon.mat"));
                coolerMat.SetFloat("_AtmosphereStrength", 20f);
                coolerMat.SetColor("_TintColor", new Color32(10, 10, 17, 255));

                meshRenderer.material = coolerMat;
            }
        }
    }   
}
