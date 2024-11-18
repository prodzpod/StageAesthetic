using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special.Colossus.TreebornColony
{
    public class Overcast : Variant
    {
        public override string[] Stages => ["habitat", "habitatfall"];
        public override string Name => nameof(Overcast);
        public override string Description => "Foggy storm.";
        public override SoundType Ambience => SoundType.Thunder;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            SetAmbientLight amb2 = volume.GetComponent<SetAmbientLight>();
            amb2.ambientSkyColor = new Color(0.5373f, 0.6354f, 0.6431f, 1);
            amb2.ambientIntensity = 0.61f;
            amb2.ApplyLighting();
            fog.fogColorEnd.value = new Color(0.3272f, 0.3711f, 0.4057f, 1);
            fog.fogColorMid.value = new Color(0.2864f, 0.2667f, 0.3216f, 0.2941f);
            fog.fogColorStart.value = new Color(0.2471f, 0.2471f, 0.2471f, 0);
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;

            if (scenename == "habitat") GameObject.Find("BHGodRay").SetActive(false);
            GameObject.Find("Directional Light (SUN)").GetComponent<Light>().color = new Color(0.7529f, 0.7137f, 0.6157f, 1);
            GameObject.Find("meshBHFog").GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/DLC2/meridian/Assets/matPMStormCloud.mat");
            GameObject wind = GameObject.Find("WindZone");
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 0.5f;
            windZone.windTurbulence = 0.65f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Extreme);
    }   
}
