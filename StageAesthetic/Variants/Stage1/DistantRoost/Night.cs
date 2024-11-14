using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage1.DistantRoost
{
    public class Night : Variant
    {
        public override string[] Stages => ["blackbeach2"];
        public override string Name => nameof(Night);
        public override string Description => "Dark and blue with green lights.";
        public override SoundType Ambience => SoundType.NightNature;
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            base.Apply(scenename, fog, cgrade, volume, loop);
            Skybox.NightSky();
            foreach (Light light in Object.FindObjectsOfType(typeof(Light)) as Light[])
            {
                var parent = light.gameObject?.transform.parent?.gameObject;
                if (!parent || (!parent.name.Equals("BbRuinBowl") && !parent.gameObject.name.Equals("BbRuinBowl (1)") || parent.gameObject.name.Equals("BbRuinBowl (2)")))
                {
                    light.intensity = 15;
                    light.range = 50;
                }
            }
            if (scenename == "blackbeach")
            {
                // Enabling some unused fog
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            DistantRoost.Vanilla.VanillaFoliage();
        }
    }
}
