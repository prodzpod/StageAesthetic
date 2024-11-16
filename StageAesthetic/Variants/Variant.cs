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
        public ConfigEntry<EnableConfig> WeatherCondition;
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
        public Variant()
        {
            Variants.Add(this);
            if (Stages.Length == 0) return; // unused
            var category = $"Stages {new string(':', (int)Hooks.SceneStage[Stages[0]])} {Hooks.SceneNames[Stages[0]]}";
            PreLoopWeight = ConfigManager.Bind(category, $"{Name} - Pre-Loop Weight", PreLoopWeightDefault, Description);
            LoopWeight = ConfigManager.Bind(category, $"{Name} - Post-Loop Weight", LoopWeightDefault, Description);
            WeatherCondition = ConfigManager.Bind(category, $"{Name} - Weather Effect", EnableConfig.Enable, Description);
            Init(category);
        }

        // statics
        public static List<Variant> Variants = [];
        public static Dictionary<string, List<Variant>> VariantsRolled = [];
        public static Variant GetVariant(string stage, bool loop = false) 
        {
            WeightedSelection<Variant> w = new();
            foreach (var v in Variants)
            {
                if (!v.Stages.Contains(stage)) continue;
                var weight = loop ? v.LoopWeight.Value : v.PreLoopWeight.Value;
                if (weight <= 0) continue; w.AddChoice(v, weight);
            }
            if (w.choices.Length == 0) return Vanilla;
            if (!Hooks.AvoidDuplicateVariants.Value) return w.Evaluate(Run.instance.stageRng.nextNormalizedFloat);
            if (!VariantsRolled.ContainsKey(stage) || VariantsRolled[stage].Count == w.choices.Length) VariantsRolled[stage] = [];
            WeightedSelection<Variant> w2 = new();
            for (int i = 0; i < w.Count; i++) if (!VariantsRolled[stage].Contains(w.choices[i].value)) w2.AddChoice(w.choices[i]);
            return w2.Evaluate(Run.instance.stageRng.nextNormalizedFloat);
        }
        public static Variant GetVariant(string stage, string name) => Variants.TryFind(x => x.Stages.Contains(stage) && x.Name.ToLower() == name.ToLower()) ?? Vanilla;
        public static Variant Vanilla => Variants.Find(x => x.Stages.Length == 0 && x.Name == "Vanilla");
    }
    public class Vanilla : Variant
    {
        public override string Name => nameof(Vanilla);
        public override void Apply(string scenename, RampFog fog, ColorGrading cgrade, PostProcessVolume volume, bool loop) { base.Apply(scenename, fog, cgrade, volume, loop); }
    }
    public enum EnableConfig
    {
        Enable,
        PreLoopOnly,
        LoopOnly,
        Disable
    }
}
