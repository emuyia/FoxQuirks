using GameNetcodeStuff;
using HarmonyLib;
using FoxQuirks.Config;

namespace FoxQuirks.Patches
{
    [HarmonyPatch]
    internal class ShipSanctuaryPatches
    {
        [HarmonyPatch(typeof(EnemyAI), "PlayerIsTargetable")]
        [HarmonyPostfix]
        private static void PlayerIsTargetable_Patch(EnemyAI __instance, PlayerControllerB playerScript, ref bool __result)
        {
            if (!FoxQuirksConfig.ShipSanctuaryEnabled.Value) return;
            if (__instance is not BushWolfEnemy) return;
            if (!__result) return;

            if (playerScript.isInHangarShipRoom)
            {
                __result = false;
            }
        }

        [HarmonyPatch(typeof(EnemyAI), "GetClosestPlayer")]
        [HarmonyPostfix]
        private static void GetClosestPlayer_Patch(EnemyAI __instance, ref PlayerControllerB __result)
        {
            if (!FoxQuirksConfig.ShipSanctuaryEnabled.Value) return;
            if (__instance is not BushWolfEnemy) return;

            if (__result != null && __result.isInHangarShipRoom)
            {
                __result = null!;
            }
        }

        [HarmonyPatch(typeof(BushWolfEnemy), "DoAIInterval")]
        [HarmonyPrefix]
        private static void BushWolfDoAIInterval_Patch(BushWolfEnemy __instance)
        {
            if (!FoxQuirksConfig.ShipSanctuaryEnabled.Value) return;
            
            if (__instance.targetPlayer != null && __instance.targetPlayer.isInHangarShipRoom)
            {
                if (__instance.currentBehaviourStateIndex == 1)
                {
                    __instance.SwitchToBehaviourState(0);
                }
            }
        }
    }
}