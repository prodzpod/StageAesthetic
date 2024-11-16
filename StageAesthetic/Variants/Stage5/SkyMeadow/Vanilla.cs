using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage5.SkyMeadow
{
    public class Vanilla: Variant
    {
        public override string[] Stages => ["skymeadow"];
        public override string Name => nameof(Vanilla);
        public override string Description => "Disabling removes vanilla from getting picked.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(239, 231, 211, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 1f;
            VanillaFoliage();
        }
        public static void VanillaFoliage()
        {
            Assets.MeshReplaceAll([
                new(["Edge Clouds"], mr => mr.gameObject.SetActive(false)),
                new(["spmSMGrass"], mr => {
                    mr.sharedMaterial.color = new Color32(236, 161, 182, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(236, 161, 182, 255);
                }),
                new(["SMVineBody"], mr => mr.sharedMaterial.color = new Color32(144, 158, 70, 255)),
                new(["spmSMHangingVinesCluster_LOD0"], mr => mr.sharedMaterial.color = new Color32(130, 150, 171, 255)),
                new(["spmBbDryBush_LOD0"], mr => {
                    mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                    if (mr.sharedMaterials.Length >= 2) mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                }),
                new(["SGMushroom"], mr => mr.sharedMaterial.color = new Color32(255, 255, 255, 255)),
            ]);
        }
    }   
}
