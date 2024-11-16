using RoR2;
using System.Drawing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public class Night: Variant
    {
        public override string[] Stages => ["frozenwall"];
        public override string Name => nameof(Night);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)");
            sun.name = "Shitty Not Working Sun";
            sun.SetActive(false);
            Skybox.NightSky(false, false, 0.4f);

            var lightCurve = new AnimationCurve(new Keyframe(1f, 1f), new Keyframe(1f, 1f));
            Assets.ReplaceAll<Light>([
                new(l => !l.gameObject.name.Contains("Light (SUN)"), l => {
                    l.type = LightType.Point;
                    l.shape = LightShape.Cone;
                    l.color = new Color32(233, 233, 190, 255);
                    if (l.GetComponent<FlickerLight>()) l.GetComponent<FlickerLight>().enabled = false;
                    if (l.GetComponent<LightIntensityCurve>())
                    {
                        var curve = l.GetComponent<LightIntensityCurve>();
                        curve.maxIntensity = 5f;
                        curve.curve = lightCurve;
                        curve.enabled = false;
                    }
                    l.intensity = 5f;
                    l.range = 65f;
                })
            ]);
            NightMaterials();
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            RallypointDelta.Vanilla.DisableRallypointSnow();
            Weather.AddSnow(Intensity.Extreme);
        }
        public static void NightMaterials()
        {
            var water = GameObject.Find("HOLDER: Skybox").transform.GetChild(0);
            water.GetComponent<MeshRenderer>().sharedMaterial = Assets.Load<Material>("RoR2/Base/goldshores/matGSWater.mat");
            var ice = Object.Instantiate(water);
            ice.transform.position = new Vector3(-1260, -115, 0);
            ice.GetComponent<MeshRenderer>().sharedMaterial = Assets.LoadRecolor("RoR2/DLC1/snowyforest/matSFIce.mat", new Color32(242, 237, 254, 216));
            Assets.MeshReplaceAll([
                new(["Terrain", "Snow"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat"))),
                new(["Stalagmite"], mr => { if (!mr.gameObject.GetComponent<Light>()) mr.gameObject.AddComponent<Light>(); })
            ]);
        }
    }
}
