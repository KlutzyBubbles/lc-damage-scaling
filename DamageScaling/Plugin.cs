using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using BepInEx.Logging;
using System;
using System.Collections.Generic;

namespace DamageScaling
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            StaticLogger = Logger;
            ConfigFile();
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_NAME);
            harmony.PatchAll(typeof(DamagePatch));
            StaticLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} loaded");
        }

        private void ConfigFile()
        {
            foreach (CauseOfDeath deathCause in Enum.GetValues(typeof(CauseOfDeath)))
            {
                ConfigEntry<float> entry = Config.Bind<float>("Scalers", deathCause.ToString(), 1f, $"Damage multiplier for {deathCause}");
                deathCauseScaling.Add(deathCause, entry.Value);
            }
        }

        public static ManualLogSource StaticLogger;

        public static Dictionary<CauseOfDeath, float> deathCauseScaling = new Dictionary<CauseOfDeath, float>();
    }
}