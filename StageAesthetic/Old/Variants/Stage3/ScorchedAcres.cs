﻿using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace StageAesthetic.Variants.Stage3
{
    internal class ScorchedAcres
    {
        public static void Sunset(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(30, 16, 52, 40);
            fog.fogColorMid.value = new Color32(123, 58, 40, 48);
            fog.fogColorEnd.value = new Color32(84, 32, 3, 222);
            fog.skyboxStrength.value = 0.076f;
            fog.fogZero.value = -0.019f;
            fog.fogOne.value = 0.211f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.6f;
            cgrade.colorFilter.value = new Color(1f, 1f, 1f, 0.08f);
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            if (WeatherEffects.Value)
            {
                lightBase.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color =  new Color32(255, 149, 128, 255);
            sunLight.intensity = 2.1f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(35f, 198.5f, 218.841f);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            Vector3 sunPosition = sunBase.parent.localPosition;
            sunPosition.y = -67;
            Transform quad = sunBase.GetChild(1);
            quad.localScale = new Vector3(14, 14, 14);
            quad.localEulerAngles = new Vector3(270, 30, 0);
            quad.localPosition = new Vector3(0, 0, 0);
            sunBase.GetChild(0).gameObject.SetActive(false);
        }

        public static void Night(RampFog fog)
        {
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
            var eclipse = FindEclipseGameObject(GameObject.Find("Weather, Wispgraveyard").scene);
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

                var coolerMat = new Material(Addressables.LoadAssetAsync<Material>("RoR2/Base/eclipseworld/matEclipseMoon.mat").WaitForCompletion());
                coolerMat.SetFloat("_AtmosphereStrength", 20f);
                coolerMat.SetColor("_TintColor", new Color32(10, 10, 17, 255));

                meshRenderer.material = coolerMat;
            }
        }

        public static void Jade(RampFog fog)
        {
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
            var eclipse = FindEclipseGameObject(GameObject.Find("Weather, Wispgraveyard").scene);
            if (eclipse != null)
            {
                eclipse.SetActive(true);
                eclipse.transform.GetChild(1).gameObject.SetActive(false); // lighting
                eclipse.transform.GetChild(2).gameObject.SetActive(false); // post-processing
                eclipse.transform.GetChild(4).GetChild(1).gameObject.SetActive(false); // dust
            }
        }

        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(36, 0, 0);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            Transform[] quadCount = new Transform[] { sunBase.GetChild(0), sunBase.GetChild(1) };
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }

        public static void SunnyBeta(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(128, 121, 99, 13);
            fog.fogColorMid.value = new Color32(106, 141, 154, 130);
            fog.fogColorEnd.value = new Color32(104, 150, 199, 255);
            fog.fogZero.value = -0.058f;
            fog.fogPower.value = 1.2f;
            fog.fogIntensity.value = 0.937f;
            cgrade.colorFilter.value = new Color32(240, 213, 248, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0f;

            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 225, 181, 255);
            sunLight.intensity = 1.6f;
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.transform.position = new Vector3(-250, 90, -199);
            sunHolder.localEulerAngles = new Vector3(20f, 57.2f, 0f);
            var sunBase = sunHolder.Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            Transform[] quadCount = new Transform[] { sunBase.GetChild(0), sunBase.GetChild(1) };
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }

        public static void CrimsonBeta(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 60);
            fog.fogColorEnd.value = new Color32(163, 74, 61, 120);
            fog.skyboxStrength.value = 0.62f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(191, 191, 191, 255);
            sunLight.intensity = 1.4f;
            sunLight.shadowStrength = 0.75f;
            sunTransform.localEulerAngles = new Vector3(65, 37, 0);
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.gameObject.SetActive(false);
            AddRain(RainType.Typhoon, true);
        }

        public static void Twilight(RampFog fog)
        {
            Skybox.SingularitySky();
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            lightBase.GetChild(2).gameObject.SetActive(false);
            lightBase.Find("Directional Light (SUN)").gameObject.SetActive(false);
            var sunHolder = lightBase.Find("CameraRelative").Find("SunHolder");
            sunHolder.gameObject.SetActive(false);
        }

        public static GameObject FindEclipseGameObject(Scene scene)
        {
            GameObject go = null;
            var rootGameObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootGameObjects.Length; i++)
            {
                var rootGameObject = rootGameObjects[i];
                if (rootGameObject.name == "Weather, Eclipse")
                {
                    go = rootGameObject;
                }
            }
            return go;
        }
    }
}