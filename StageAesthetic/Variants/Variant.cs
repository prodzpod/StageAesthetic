using BepInEx.Bootstrap;
using BepInEx.Configuration;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    public abstract class Variant
    {
        // instanced
        public virtual float PreLoopWeightDefault => 1;
        public ConfigEntry<float> PreLoopWeight;
        public virtual float LoopWeightDefault => 1;
        public ConfigEntry<float> LoopWeight;
        public ConfigEntry<EnableConfig> WeatherCondition = null;
        public virtual string[] Stages => [];
        // IMPORTANT: the first entry MUST be a normal stage or intermission, simulacrum variants go second
        public virtual string Name => "";
        public virtual string Description => "";
        public virtual SoundType Ambience => SoundType.None;
        public virtual void Init(string category) { }
        public virtual void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop)
        {
            Main.Log.LogInfo($"Loading {Hooks.SceneNames[scenename]} ({Name})");
            Weather.StopSounds();
            Weather.PlaySound(Ambience);
            if (WeatherCondition == null) return;
            var doWeather = WeatherCondition.Value switch
            {
                EnableConfig.Enable => true,
                EnableConfig.PreLoopOnly => !loop,
                EnableConfig.LoopOnly => loop,
                EnableConfig.Disable => false,
                _ => false,
            };
            if (doWeather) DoWeather(scenename, fog, cgrade, volume, loop);
        }
        public virtual void DoWeather(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop) { }
        public Variant() { Variants.Add(this); }
        public void InitConfig()
        {
            if (Stages.Length == 0 || !Hooks.SceneNames.ContainsKey(Stages[0])) return;
            var category = $"Stages {new string(':', (int)Hooks.SceneStage[Stages[0]])} {Hooks.SceneNames[Stages[0]]}";
            PreLoopWeight = ConfigManager.Bind(category, $"{Name} - Pre-Loop Weight", PreLoopWeightDefault, Description);
            LoopWeight = ConfigManager.Bind(category, $"{Name} - Post-Loop Weight", LoopWeightDefault, Description);
            WeatherCondition = ConfigManager.Bind(category, $"{Name} - Weather Effect", EnableConfig.Enable, "Enables weather effects for this variant.");
            Init(category);
        }

        // statics
        public static List<Variant> Variants = [];
        public static Dictionary<string, List<Variant>> VariantsRolled = [];
        public static Variant GetVariant(string stage, bool loop = false) 
        {
            WeightedSelection<Variant> w = new();
            int c = 0;
            foreach (var v in Variants)
            {
                if (!v.Stages.Contains(stage)) continue;
                var weight = loop ? v.LoopWeight.Value : v.PreLoopWeight.Value;
                if (weight <= 0) continue; c++; w.AddChoice(v, weight);
            }
            if (LoopVariantEnabled() || c == 0) return Vanilla;
            if (!Hooks.AvoidDuplicateVariants.Value) return w.Evaluate(Run.instance.stageRng.nextNormalizedFloat);
            if (!VariantsRolled.ContainsKey(stage) || VariantsRolled[stage].Count >= c) VariantsRolled[stage] = [];
            WeightedSelection<Variant> w2 = new();
            for (int i = 0; i < w.Count; i++) if (!VariantsRolled[stage].Contains(w.choices[i].value)) w2.AddChoice(w.choices[i]);
            var v2 = w2.Evaluate(Run.instance.stageRng.nextNormalizedFloat); VariantsRolled[stage].Add(v2);
            return v2;
        }
        public static Variant GetVariant(string stage, string name) => Variants.TryFind(x => x.Stages.Contains(stage) && x.Name.ToLower() == name.ToLower()) ?? Vanilla;
        public static Variant Vanilla => Variants.Find(x => x.Stages.Length == 0 && x.Name == "Vanilla");
        public static bool LoopVariantEnabled()
        {
            if (!Run.instance || !Chainloader.PluginInfos.ContainsKey("Wolfo.LoopVariants")) return false;
            string[] vanillaLoopVariants = ["lakesnight", "villagenight", "habitatfall"];
            if (vanillaLoopVariants.Contains(SceneCatalog.currentSceneDef.cachedName)) return false;
            return LoopVariantEnabledInternal();
        }
        public static bool LoopVariantEnabledInternal() => Run.instance.GetComponent<LoopVariants.LoopVariantsMain.SyncLoopWeather>().CurrentStage_LoopVariant;
    }
    public class Vanilla : Variant
    {
        public override string Name => nameof(Vanilla);
        public override string[] Stages => [];
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop) { }
    }
    public enum EnableConfig
    {
        Enable,
        PreLoopOnly,
        LoopOnly,
        Disable
    }
}
