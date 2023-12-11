using GameNetcodeStuff;
using HarmonyLib;

namespace DamageScaling
{
    internal class DamagePatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayer")]
        [HarmonyPrefix]
        private static void DamagePlayerPrefix(ref int damageNumber, CauseOfDeath causeOfDeath, bool fallDamage)
        {
            Plugin.StaticLogger.LogInfo($"DamagePlayerPrefix: {damageNumber} : {causeOfDeath} : {fallDamage}");
            CauseOfDeath localCause = causeOfDeath;
            if (fallDamage)
            {
                localCause = CauseOfDeath.Gravity;
            }
            damageNumber = (int)(damageNumber * Plugin.deathCauseScaling[localCause]);
        }
    }
}
