using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StageAesthetic
{
    public class TitleScreen
    {
        public static ConfigEntry<bool> Enable;
        public static void Init()
        {
            Enable = ConfigManager.Bind("General", "Alter title screen?", true, "Adds rain, patches of grass, particles and brings a Commando closer to focus.");
            SceneManager.sceneLoaded += Hook;
        }
        public static void Hook(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "title" || !Enable.Value) return;
            var menuBase = GameObject.Find("MainMenu").transform;
            var graphicBase = GameObject.Find("HOLDER: Title Background").transform;
            graphicBase.Find("Terrain").gameObject.SetActive(true);
            graphicBase.Find("CamDust").gameObject.SetActive(true);
            // graphicBase.Find("Misc Props").Find("DeadCommando").localPosition = new Vector3(16, -2f, 27);
            var menuRain = menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject.GetComponent<ParticleSystem>();
            var emission = menuRain.emission;
            var rateOverTime = emission.rateOverTime;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve()
            {
                constant = 100,
                constantMax = 100,
                constantMin = 60,
                curve = rateOverTime.curve,
                curveMax = rateOverTime.curveMax,
                curveMin = rateOverTime.curveMax,
                curveMultiplier = rateOverTime.curveMultiplier,
                mode = rateOverTime.mode
            };

            var colorOverLifetime = menuRain.colorOverLifetime;
            colorOverLifetime.enabled = false;

            menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").eulerAngles = new Vector3(80, 90, 0);
            var menuWind = GameObject.Find("HOLDER: Title Background").transform.Find("FX").Find("WindZone").gameObject.GetComponent<WindZone>();
            menuWind.windMain = 0.5f;
            menuWind.windTurbulence = 1;
            Weather.StopSounds();
            Weather.PlaySound(SoundType.Wind);
        }
    }
}
