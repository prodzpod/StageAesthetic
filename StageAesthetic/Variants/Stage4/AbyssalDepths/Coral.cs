using RoR2;
using StageAesthetic.Variants.Stage3.SulfurPools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Coral : Variant
    {
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Coral);
        public override string Description => "Texture swap to Blue/Purple/Pink Sundered Grove.";
        public override SoundType Ambience => SoundType.Wind;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            fog.fogColorStart.value = new Color32(127, 70, 206, 0);
            fog.fogColorMid.value = new Color32(185, 72, 119, 50);
            fog.fogColorEnd.value = new Color32(183, 93, 129, 125);
            GameObject.Find("Directional Light (SUN)").transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(130, 163, 175, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.75f;
            Assets.ReplaceAll<Light>([
                new(l => !l.name.Contains("Light (SUN)"), l => {
                    l.color = new Color32(216, 192, 32, 255);
                    l.intensity = 25f;
                    l.range = 30f;
                }),
                new(l => l.gameObject.GetComponent<FlickerLight>(), l => l.gameObject.GetComponent<FlickerLight>().enabled = false)
            ]);
            GameObject.Find("DCPPInTunnels").SetActive(false);
            // terrain detail, tree?, ruins
            Common.SimMaterials(
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCTerrainWallsInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianRuinsHeavyInfiniteTower.mat"),
                Assets.Load<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat")
            );
        }
        public override void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
            => Weather.AddRain(Intensity.Mild);
    }   
}
