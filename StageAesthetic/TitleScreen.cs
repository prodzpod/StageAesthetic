using BepInEx.Configuration;
using UnityEngine.SceneManagement;

namespace StageAesthetic
{
    public class TitleScreen
    {
        public static ConfigEntry<bool> Enable;
        public static void Init()
        {
            Enable = ConfigManager.Bind("General", "Alter title screen?", true, "Adds rain, patches of grass, particles and brings a Commando closer to focus.");
            if (Enable.Value) SceneManager.sceneLoaded += Hook;
        }
        public static void Hook(Scene scene, LoadSceneMode mode)
        {

        }
    }
}
