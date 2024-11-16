using UnityEngine;

namespace StageAesthetic.Variants.Stage2.AbandonedAqueduct
{
    public static class Common
    {
        public static void VanillaFoliage()
            => Assets.ReplaceAll<LineRenderer>([new(_ => true, lr => lr.sharedMaterial.color = new Color32(141, 42, 42, 255))]);
    }
}
