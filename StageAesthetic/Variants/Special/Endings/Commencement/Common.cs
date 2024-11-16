using UnityEngine;

namespace StageAesthetic.Variants.Special.Endings.Commencement
{
    public static class Common
    {
        public static void ChangeFlames(Material flameMat, Color flameColor)
        {
            Assets.MeshReplaceAll([
                new(["BazaarLight", "mdlLunarCoolingBowlLarge"], mr => {
                    if (!mr.sharedMaterial) return;
                    ParticleSystemRenderer fire = mr.gameObject.transform.GetComponentInChildren<ParticleSystemRenderer>();
                    if (!fire) return;
                    fire.sharedMaterial = flameMat;
                    Light fireLight = mr.gameObject.transform.GetComponentInChildren<Light>();
                    if (fireLight)
                        fireLight.color = flameColor;
                })
            ]);
        }
    }
}
