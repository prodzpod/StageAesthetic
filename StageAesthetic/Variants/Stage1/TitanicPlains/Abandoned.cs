using StageAesthetic.Variants.Stage1.DistantRoost;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.TitanicPlains
{
    public class Abandoned : Variant
    {
        public override string[] Stages => ["golemplains", "golemplains2"];
        public override string Name => nameof(Abandoned);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(1f, 0.65f, 0.5f, 1f);
            sun.intensity = 1f;

            RampFog rampFog = Assets.GooLakeProfile.GetSetting<RampFog>();

            fog.fogColorStart.value = new Color32(150, 93, 118, 19);
            fog.fogColorMid.value = new Color32(173, 126, 84, 199);
            fog.fogColorEnd.value = new Color32(222, 153, 123, 255);
            fog.fogZero.value = 0f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 1f;
            fog.fogOne.value = 0.15f;
            fog.skyboxStrength.value = 0.08f;

            GameObject.Find("FOLIAGE: Grass").SetActive(false);
            var water = GameObject.Find("HOLDER: Water").transform.GetChild(0);
            water.localPosition = new Vector3(-564.78f, -170f, 133.4f);
            water.localScale = new Vector3(200f, 200f, 200f);
            Nostalgic.VanillaFoliage();
            Assets.MeshReplaceAll([
                new(["Terrain", "Wall North"], mr => Assets.TryMeshReplace(mr, Assets.LoadRecolor("RoR2/Base/goolake/matGoolakeTerrain.mat", new Color32(230, 223, 174, 219)))),
                new(["Rock", "Boulder", "mdlGeyser"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat"))),
                new(["Ring"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat"))),
                new(["Block", "MiniBridge", "Circle"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/goolake/matGoolakeStoneTrim.mat"))),
                new(["Plane1x1x100x100"], mr => Assets.TryMeshReplace(mr, Assets.Load<Material>("RoR2/Base/goolake/matGoolake.mat"))),
            ]);
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddSand(Intensity.Extreme);
    }
}
