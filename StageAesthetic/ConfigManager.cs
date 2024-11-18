using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using StageAesthetic.Variants;
using System;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using UnityEngine;

namespace StageAesthetic
{
    public class ConfigManager // the Return of the King
    {
        public static ConfigFile Config;
        public static ConfigFile BackupConfig;
        public static ConfigEntry<bool> AutoUpdate;
        public static void Init()
        {
            Config = new ConfigFile(System.IO.Path.Combine(Paths.ConfigPath, Main.PluginGUID + ".cfg"), true);
            BackupConfig = new ConfigFile(System.IO.Path.Combine(Paths.ConfigPath, Main.PluginGUID + ".Backup.cfg"), true);
            AutoUpdate = Config.Bind("Config", "Enable Auto Config Sync", true, "disable this to keep all default value as is instead of auto updating to future versions (manually changed value will not change regardless)");
            BackupConfig.Bind(": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :");
            var icon = Main.AssetBundle.LoadAsset<Sprite>("texModIcon.png");
            ModSettingsManager.SetModIcon(icon, "StageAesthetic.TabID", "Stage Aesthetics");
            foreach (var _e in Enum.GetValues(typeof(Stage))) {
                Stage e = (Stage)_e;
                ModSettingsManager.SetModIcon(icon, $"StageAesthetic.TabID.{(int)e}", $"SA: {e}");
            }
        }
        public static ConfigEntry<T> Bind<T>(string category, string name, T def, string desc)
        {
            var config = Config.Bind(Util.ConfigSafe(category), Util.ConfigSafe(name), def, desc);
            var backup = BackupConfig.Bind(Util.ConfigSafe(category), Util.ConfigSafe(name), def, desc);
            if (AutoUpdate.Value && backup.BoxedValue == config.BoxedValue && backup.DefaultValue != backup.BoxedValue)
            {
                Main.Log.LogInfo("Default Config Auto-Updated for " + config);
                config.BoxedValue = config.DefaultValue;
                backup.BoxedValue = backup.DefaultValue;
            }
            var _e = category.Occurance(':');
            var _id = "StageAesthetic.TabID";
            var _name = "Stage Aesthetics";
            if (_e != 0) { var e = (Stage)_e; _id = "StageAesthetic.TabID." + _e; _name = "SA: " + e; }
            BaseOption option = null;
            switch (config)
            {
                case ConfigEntry<bool> _bc: option = new CheckBoxOption(_bc, new CheckBoxConfig() { restartRequired = true }); break;
                case ConfigEntry<float> _fc: option = new SliderOption(_fc, new SliderConfig() { min = 0, max = 10, restartRequired = true, FormatString = "{0:2}" }); break;
                case ConfigEntry<EnableConfig> _wc: option = new ChoiceOption(_wc, new ChoiceConfig() { restartRequired = true }); break;
                default: Main.Log.LogWarning("Undefined Config Type " + typeof(T).Name + ", not added to ROO"); break;
            }
            if (option != null) ModSettingsManager.AddOption(option, _id, _name);
            return config;
        }
    }
}
