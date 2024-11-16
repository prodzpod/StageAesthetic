using UnityEngine;

namespace StageAesthetic.Variants.Stage3.SulfurPools
{
    public static class Common
    {
        public static void VanillaWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            if (waterBlue && waterGreen && waterRed && waterYellow)
            {
                waterBlue.color = new Color32(255, 219, 0, 255);
                waterGreen.color = new Color32(255, 219, 0, 255);
                waterRed.color = new Color32(207, 0, 148, 255);
                waterYellow.color = new Color32(255, 219, 0, 255);
            }
        }
    }
}
