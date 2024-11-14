using BepInEx.Configuration;
using RoR2;
using UnityEngine;

namespace StageAesthetic
{
    public class Weather
    {
        public static GameObject Rain;
        public static GameObject Snow;
        public static GameObject Sand;
        public static ConfigEntry<bool> WeatherSound;
        public static void Init()
        {
            Rain = Main.AssetBundle.LoadAsset<GameObject>("Stage Aesthetic Rain.prefab");
            Rain.transform.eulerAngles = new Vector3(90, 0, 0);
            Snow = Main.AssetBundle.LoadAsset<GameObject>("Stage Aesthetic Snow.prefab");
            Snow.transform.eulerAngles = new Vector3(90, 0, 0);
            Sand = Main.AssetBundle.LoadAsset<GameObject>("Stage Aesthetic Sand.prefab");
            WeatherSound = ConfigManager.Bind("General", "Use weather sound effects?", true, "Adds sound effects for weather.");
        }
        public static void PlaySound(SoundType soundType)
        {
            if (!WeatherSound.Value || soundType == SoundType.None) return;
            var soundToPlay = soundType switch
            {
                SoundType.DayNature => "Play_SA_birds",
                SoundType.Rain => "Play_SA_rain",
                SoundType.Thunder => "Play_SA_thunder",
                SoundType.NightNature => "Play_SA_night",
                SoundType.WaterStream => "Play_SA_water",
                SoundType.Wind => "Play_SA_wind",
                SoundType.Void => "Play_SA_void",
                _ => "Play_SA_wind"
            };
            RoR2.Util.PlaySound(soundToPlay, RoR2Application.instance.gameObject);
        }

        public static void StopSounds()
        {
            RoR2.Util.PlaySound("Stop_SA_birds", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_rain", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_thunder", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_night", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_water", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_wind", RoR2Application.instance.gameObject);
            RoR2.Util.PlaySound("Stop_SA_void", RoR2Application.instance.gameObject);
        }
        public static void AddRain(Intensity intensity, bool bloodRain = false)
        {
            if (!Rain || !Run.instance) return;
            if (!Rain.GetComponent<StageAestheticWeatherController>())
                Rain.AddComponent<StageAestheticWeatherController>();

            var actualRain = Rain.transform.GetChild(0).gameObject;
            var particleSystemRenderer = actualRain.GetComponent<ParticleSystemRenderer>();
            var rainMaterial = particleSystemRenderer.material;
            rainMaterial.shader = Assets.CloudRemap;
            rainMaterial.EnableKeyword("DISABLEREMAP");
            rainMaterial.SetFloat("_DstBlend", 10);
            rainMaterial.SetFloat("_SrcBlend", 5);
            rainMaterial.SetColor("_TintColor", bloodRain ? new Color32(72, 36, 36, 255) : new Color32(166, 166, 166, 255));

            WeatherData data = intensity switch {
                Intensity.Mild => new(270, 300, 110, 120, 6),
                Intensity.Medium => new(310, 340, 130, 140, 10),
                Intensity.Heavy => new(350, 380, 150, 160, 14),
                Intensity.Extreme => new(390, 420, 170, 180, 19),
                _ => new()
            };
            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;
            var magnitude = data.Magnitude + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            var speed = data.Speed + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);

            var particleSystem = actualRain.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1000f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(magnitude * 30f));
            var rateOverTime = particleSystem.emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, 800f + magnitude);
            var shape = particleSystem.shape;
            shape.rotation = new Vector3(data.Angle, 0f, data.Angle);

            Object.Instantiate(Rain, Vector3.zero, Quaternion.identity);
        }

        public static void AddSnow(Intensity intensity, float heightOverride = 150f)
        {
            if (!Snow || !Run.instance) return;
            if (!Snow.GetComponent<StageAestheticWeatherController>())
                Snow.AddComponent<StageAestheticWeatherController>();

            var actualSnow = Snow.transform.GetChild(0).gameObject;
            actualSnow.transform.localPosition = new Vector3(0f, heightOverride, 0f);
            var goofySnow = actualSnow.transform.GetChild(0).gameObject;
            var snowMaterial = actualSnow.GetComponent<ParticleSystemRenderer>().material;
            snowMaterial.shader = Assets.CloudRemap;
            snowMaterial.EnableKeyword("DISABLEREMAP");
            snowMaterial.SetFloat("_DstBlend", 10);
            snowMaterial.SetFloat("_SrcBlend", 5);
            var goofySnowMaterial = goofySnow.GetComponent<ParticleSystemRenderer>().material;
            goofySnowMaterial.shader = Assets.CloudRemap;
            goofySnowMaterial.EnableKeyword("DISABLEREMAP");
            goofySnowMaterial.SetFloat("_DstBlend", 10);
            goofySnowMaterial.SetFloat("_SrcBlend", 5);

            WeatherData data = intensity switch
            {
                Intensity.Mild => new(600, 800, 12, 16, 7),
                Intensity.Medium => new(1400, 1700, 17, 21, 12),
                Intensity.Heavy => new(2300, 2800, 22, 28, 17),
                Intensity.Extreme => new(3400, 3800, 29, 33, 25),
                _ => new()
            };
            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;
            var magnitude = data.Magnitude + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            var speed = data.Speed + Mathf.Sqrt(difficultyDefScalingValue) + Mathf.Sqrt(difficultyCoefficient);

            var particleSystem = actualSnow.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1000f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(magnitude * 5f));
            var rateOverTime = particleSystem.emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, magnitude);
            var shape = particleSystem.shape;
            shape.rotation = new Vector3(data.Angle, 0f, data.Angle);

            // Debug.LogErrorFormat("Rain Type {0} Rain Intensity {1} Rain Speed {2} Max Particles {3} Rate Over Time Constant {4} Difficulty Def Scaling Value {5} Difficulty Coefficient {6}", rainType, intensity, speed, main.maxParticles, rateOverTime.constant, difficultyDefScalingValue, difficultyCoefficient);
            Object.Instantiate(Snow, Vector3.zero, Quaternion.identity);
        }

        public static void AddSand(Intensity intensity)
        {
            if (!Sand || !Run.instance) return;
            if (!Sand.GetComponent<StageAestheticWeatherController>())
                Sand.AddComponent<StageAestheticWeatherController>();

            var actualSand = Sand.transform.GetChild(0).gameObject;
            var sandMaterial = actualSand.GetComponent<ParticleSystemRenderer>().material;
            sandMaterial.shader = Assets.CloudRemap;
            sandMaterial.EnableKeyword("DISABLEREMAP");
            sandMaterial.SetFloat("_DstBlend", 10);
            sandMaterial.SetFloat("_SrcBlend", 5);

            WeatherData data = intensity switch
            {
                Intensity.Mild => new(2500, 3000, 20, 25, 7),
                Intensity.Medium => new(3000, 3500, 25, 30, 12),
                Intensity.Heavy => new(3500, 4000, 25, 30, 17),
                Intensity.Extreme => new(4500, 5000, 25, 30, 25),
                _ => new()
            };
            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;
            var magnitude = data.Magnitude + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            var speed = data.Speed + Mathf.Sqrt(difficultyDefScalingValue * 5f) + Mathf.Sqrt(difficultyCoefficient / 2f);

            var particleSystem = actualSand.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1100f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(magnitude * 5f));
            var rateOverTime = particleSystem.emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, magnitude);
            var shape = particleSystem.shape;
            shape.rotation = new Vector3(data.Angle, 0f, data.Angle);

            // Debug.LogErrorFormat("Rain Type {0} Rain Intensity {1} Rain Speed {2} Max Particles {3} Rate Over Time Constant {4} Difficulty Def Scaling Value {5} Difficulty Coefficient {6}", rainType, intensity, speed, main.maxParticles, rateOverTime.constant, difficultyDefScalingValue, difficultyCoefficient);
            Object.Instantiate(Sand, Vector3.zero, Quaternion.identity);
        }

        public struct WeatherData(float magnitudeMin, float magnitudeMax, float speedMin, float speedMax, float angle)
        {
            public float magnitudeMin = magnitudeMin;
            public float magnitudeMax = magnitudeMax;
            public float speedMin = speedMin;
            public float speedMax = speedMax;
            public float angleMin = angle;
            public float angleMax = angle;
            public readonly float Magnitude => Run.instance.runRNG.RangeFloat(magnitudeMin, magnitudeMax);
            public readonly float Speed => Run.instance.runRNG.RangeFloat(speedMin, speedMax);
            public readonly float Angle => Run.instance.runRNG.RangeFloat(angleMin, angleMax);
        }
    }
    public enum SoundType
    {
        None,
        DayNature,
        Rain,
        Thunder,
        NightNature,
        WaterStream,
        Wind,
        Void
    }
    public enum Intensity
    {
        None,
        Mild,
        Medium,
        Heavy,
        Extreme
    }

    // for compat
    public class StageAestheticWeatherController : MonoBehaviour
    {
        public GameObject particleSystem;
        public bool disable = false;
        public float timer = 0f;
        public float interval = 0.1f;

        public void Start()
        {
            particleSystem = transform.GetChild(0).gameObject;
        }

        public void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer >= interval)
            {
                if (disable)
                    particleSystem.SetActive(false);
                else
                    particleSystem.SetActive(true);
                timer = 0f;
            }
        }
    }
}
