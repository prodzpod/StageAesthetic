using StageAesthetic.Variants.Stage3.SulfurPools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage4.AbyssalDepths
{
    public class Night : Variant
    {
        public override string[] Stages => ["dampcavesimple"];
        public override string Name => nameof(Night);
        public override string Description => "Dark blue with light fog.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();
            GameObject.Find("CEILING").SetActive(false);
            GameObject.Find("SceneInfo").GetComponent<PostProcessVolume>().enabled = false;
            GameObject.Find("Directional Light (SUN)").SetActive(false);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(67, 65, 109, 76);
            caveFog.fogColorMid.value = new Color32(40, 68, 123, 161);
            caveFog.fogColorEnd.value = new Color32(46, 128, 148, 200);
            // cgrade.colorFilter.value = new Color32(119, 207, 181, 255);
            //cgrade.colorFilter.overrideState = true;
            // Lighting: Blue coral, cyan or green lighting otherwise
            Common.LightChange(new Color(0.188f, 0.444f, 0, 1), new Color(0.181f, 0.921f, 0.945f), new Color(0f, 0.837f, 0.14f));
        }
    }   
}
