using BepInEx.Configuration;
using RoR2;
using RoR2.UI;
using StageAesthetic.Variants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace StageAesthetic
{
    public class Hooks
    {
        public static Dictionary<string, string> SceneNames = [];
        public static Dictionary<string, Stage> SceneStage = [];
        public static ConfigEntry<bool> DisplayVariantName;
        public static string currentVariantName = "";
        public static void Init()
        {
            Assets.CloudRemap = Assets.Load<Shader>("RoR2/Base/Shaders/HGCloudRemap.shader");
            Assets.SnowTopped = Assets.Load<Shader>("RoR2/Base/Shaders/HGSnowTopped.shader"); // who's snow and why are they being topped
            Assets.GooLakeProfile = Assets.Load<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGoolake.asset");
            TitleScreen.Init();
            Weather.Init();
            DisplayVariantName = ConfigManager.Bind("General", "Display Variant Name", true, "Display the variant name in the stage text.");
        }
        public static void PostInit()
        {
            for (int i = 1; i <= 5; i++)
            {
                SceneCollection sg = Assets.Load<SceneCollection>("RoR2/Base/SceneGroups/sgStage" + i + ".asset");
                foreach (SceneCollection.SceneEntry entry in sg.sceneEntries) SceneStage[entry.sceneDef.cachedName] = (Stage)i;
            }
            foreach (var s in SceneCatalog.allStageSceneDefs)
            {
                SceneNames[s.cachedName] = Language.GetString(s.nameToken);
                if (!SceneStage.ContainsKey(s.cachedName)) SceneStage[s.cachedName] = Stage.Special;
            }
            foreach (var t in Util.FindAllDerivedTypes<Variant>()) t.GetConstructor([]).Invoke(null);
        }
        public static void RollVariant(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            foreach (var a in Assets.CacheMaterial.Values) Object.Destroy(a); 
            Assets.CacheMaterial.Clear();
            var sceneName = SceneManager.GetActiveScene().name;
            var currentScene = SceneInfo.instance;
            PostProcessVolume volume = currentScene?.GetComponent<PostProcessVolume>();
            string[] names = ["PP + Amb", "PP, Global", "GlobalPostProcessVolume, Base", "PP+Amb"];
            foreach (var name in names) volume = TryAlternative(volume, name);
            volume = TryAlternative(volume, GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("SandOvercast")?.gameObject);
            if (sceneName == "moon2")
            {
                volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                volume.profile.AddSettings<RampFog>();
                volume.enabled = true;
                volume.isGlobal = true;
                volume.priority = 9999f;
            }
            if (!volume || !volume.isActiveAndEnabled) { Main.Log.LogWarning("PPV Not Found, skipping"); orig(self); return; }
            var rampFog = volume.profile.GetSetting<RampFog>();
            var colorGrading = volume.profile.GetSetting<ColorGrading>() ?? volume.profile.AddSettings<ColorGrading>();
            var loop = Run.instance.loopClearCount > 0;
            var v = Variant.GetVariant(sceneName, loop);
            v.Apply(sceneName, rampFog, colorGrading, volume, loop);
            volume.profile.name = "SA Profile" + " (" + v.Name + ")";
            orig(self);
            currentVariantName = self.teleporterInstance ? v.Name : "";
        }
        public static void AppendStageToken(On.RoR2.UI.AssignStageToken.orig_Start orig, AssignStageToken self)
        {
            orig(self);
            if (DisplayVariantName.Value && currentVariantName != "")
                self.titleText.text += " (" + currentVariantName + ")";
        }
        public static PostProcessVolume TryAlternative(PostProcessVolume volume, string name)
            => TryAlternative(volume, GameObject.Find(name));
        public static PostProcessVolume TryAlternative(PostProcessVolume volume, GameObject alt)
        {
            if (volume && volume.isActiveAndEnabled) return volume;
            if (alt) return alt.GetComponent<PostProcessVolume>();
            return null;
        }
    }
}
