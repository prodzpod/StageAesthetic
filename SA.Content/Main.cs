using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using RoR2;
using UnityEngine;
[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace StageAesthetic
{
    [BepInPlugin(PluginAuthor, PluginName, PluginVersion)]
    [BepInDependency(RiskOfOptions.PluginInfo.PLUGIN_GUID)]
    [BepInDependency("JaceDaDorito.FBLStage", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("PlasmaCore.ForgottenRelics", BepInDependency.DependencyFlags.SoftDependency)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "HIFU";
        public const string PluginName = "StageAesthetic";
        public const string PluginVersion = "1.1.0";
        public static ManualLogSource Log;
        public static PluginInfo pluginInfo;
        public static ConfigFile Config;
        public static Main Instance;
        private static AssetBundle _assetBundle;
        public static AssetBundle AssetBundle
        {
            get
            {
                if (_assetBundle == null)
                    _assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pluginInfo.Location), "stageaesthetic"));
                return _assetBundle;
            }
        }

        public void Awake()
        {
            Instance = this;
            pluginInfo = Info;
            Log = Logger;
            ConfigManager.Init();
            Hooks.Init();
            RoR2Application.onLoad += Hooks.PostInit;
            On.RoR2.SceneDirector.Start += Hooks.RollVariant;
            On.RoR2.UI.AssignStageToken.Start += Hooks.AppendStageToken;
        }
    }
}
