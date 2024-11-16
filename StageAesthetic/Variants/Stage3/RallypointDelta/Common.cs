using UnityEngine;

namespace StageAesthetic.Variants.Stage3.RallypointDelta
{
    public static class Common
    {
        public static void DisableRallypointSnow()
        {
            var snowParticles = GameObject.Find("CAMERA PARTICLES: SnowParticles").gameObject;
            snowParticles.SetActive(false);
        }
    }
}
