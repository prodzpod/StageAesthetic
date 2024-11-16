using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage3.ScorchedAcres
{
    public static class Common
    {
        public static GameObject FindEclipseGameObject(Scene scene)
        {
            GameObject go = null;
            var rootGameObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootGameObjects.Length; i++)
            {
                var rootGameObject = rootGameObjects[i];
                if (rootGameObject.name == "Weather, Eclipse")
                {
                    go = rootGameObject;
                }
            }
            return go;
        }
    }
}
