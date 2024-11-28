using BepInEx.Configuration;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.UI;
using StageAesthetic.Variants;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace StageAesthetic
{
    public class Hooks : NetworkBehaviour
    {
        public static Dictionary<string, string> SceneNames = [];
        public static Dictionary<string, Stage> SceneStage = [];
        public static ConfigEntry<bool> DisplayVariantName;
        public static ConfigEntry<bool> AvoidDuplicateVariants;
        public static string currentVariantName = "";
        public static void Init()
        {
            Main.Log.LogInfo("init");
            Assets.CloudRemap = Assets.Load<Shader>("RoR2/Base/Shaders/HGCloudRemap.shader");
            Assets.SnowTopped = Assets.Load<Shader>("RoR2/Base/Shaders/HGSnowTopped.shader"); // who's snow and why are they being topped
            Assets.GooLakeProfile = Assets.Load<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGoolake.asset");
            TitleScreen.Init();
            Weather.Init();
            DisplayVariantName = ConfigManager.Bind("General", "Display Variant Name", true, "Display the variant name in the stage text.");
            AvoidDuplicateVariants = ConfigManager.Bind("General", "Avoid Duplicate Variants", true, "Remove the variant from the pool once it is rolled until all variant for that stage is rolled.");
            Variants.Stage5.SkyMeadow.Common.AddHook();
            foreach (var t in Util.FindAllDerivedTypes<Variant>()) t.GetConstructor([]).Invoke(null);
            NetworkingAPI.RegisterMessageType<HookClient>();
        }
        public static void PostInit()
        {
            Main.Log.LogInfo("Postinit");
            Assets.Init();
            for (int i = 1; i <= 5; i++)
            {
                SceneCollection sg = Assets.Load<SceneCollection>("RoR2/Base/SceneGroups/sgStage" + i + ".asset");
                foreach (SceneCollection.SceneEntry entry in sg.sceneEntries) SceneStage[entry.sceneDef.cachedName] = (Stage)i;
            }
            foreach (var s in new string[] { "moon", "moon2", "voidraid", "mysteryspace", "limbo", "BulwarksHaunt_GhostWave" }) SceneStage[s] = Stage.Ending;
            foreach (var s in SceneCatalog.allStageSceneDefs)
            {
                SceneNames[s.cachedName] = Language.GetString(s.nameToken).Replace(": ", " - ");
                if (!SceneStage.ContainsKey(s.cachedName)) SceneStage[s.cachedName] = Stage.Special;
            }
            foreach (var v in Variant.Variants) v.InitConfig();
        }
        public static void RollVariant(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            if (NetworkServer.active) RollVariantInternal();
            orig(self);
        }
        public static Variant RollVariantInternal(string forceScene = "", string forceVariant = "")
        {
            currentVariantName = "";
            Variants.Stage5.SkyMeadow.Common.MaulingRockOverride = null;
            Variants.Stage5.SkyMeadow.Common.Rocks = [];
            foreach (var a in Assets.CacheMaterial.Values) UnityEngine.Object.Destroy(a);
            Assets.CacheMaterial.Clear();
            var sceneName = string.IsNullOrWhiteSpace(forceScene) ? SceneManager.GetActiveScene().name : forceScene;
            var currentScene = SceneInfo.instance;
            PostProcessVolume volume = currentScene?.GetComponent<PostProcessVolume>();
            string[] names = ["PP + Amb", "PP, Global", "GlobalPostProcessVolume, Base", "PP+Amb"];
            foreach (var name in names) volume = TryAlternative(volume, name);
            volume = TryAlternative(volume, GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("SandOvercast")?.gameObject);
            volume = TryAlternative(volume, GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("Sandstorm")?.gameObject);
            if (!volume && sceneName == "moon2")
            {
                volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                volume.profile.AddSettings<RampFog>();
                volume.enabled = true;
                volume.isGlobal = true;
                volume.priority = 9999f;
            }
            if (!volume || !volume.isActiveAndEnabled) { Main.Log.LogWarning("PPV Not Found, skipping"); return Variant.Vanilla; }
            var rampFog = volume.profile.GetSetting<RampFog>();
            var colorGrading = volume.profile.GetSetting<ColorGrading>() ?? volume.profile.AddSettings<ColorGrading>();
            var loop = (Run.instance?.loopClearCount ?? 0) > 0;
            var v = string.IsNullOrWhiteSpace(forceVariant) ? Variant.GetVariant(sceneName, loop) : Variant.GetVariant(sceneName, forceVariant);
            if (NetworkServer.active) new HookClient(v.Name).Send(NetworkDestination.Clients);
            if (string.IsNullOrWhiteSpace(forceVariant))
            {
                currentVariantName = v.Name;
                volume.profile.name = "SA Profile" + " (" + v.Name + ")";
            }
            try { v.Apply(sceneName, rampFog, colorGrading, volume, loop); }
            catch (Exception e) { Main.Log.LogError(e);  Main.Log.LogWarning("Variant application failed! some stuff may look weird..."); }
            currentVariantName = v == Variant.Vanilla ? "" : v.Name;
            return v;
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
        public class HookClient(string variant) : INetMessage
        {
            public HookClient() : this("") { }
            public string variant = variant;
            public void Serialize(NetworkWriter writer) { writer.Write(variant); }
            public void Deserialize(NetworkReader reader) { variant = reader.ReadString(); }
            public void OnReceived() { if (!string.IsNullOrWhiteSpace(variant) && !NetworkServer.active) RollVariantInternal(SceneCatalog.currentSceneDef.cachedName, variant); }
        }
    }
}
