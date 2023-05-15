using HarmonyLib;
using LockHoldables;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static TransferrableObject;

namespace Lock__instances.Patches
{
    /// <summary>
    /// This is an example patch, made to demonstrate how to use Harmony. You should remove it if it is not used.
    /// </summary>
    [HarmonyPatch(typeof(TransferrableObject))]
    [HarmonyPatch("OnRelease")]
    [HarmonyPatch(new Type[] { typeof(DropZone), typeof(GameObject)})]
    internal class HoldPatch
    {
        private static bool Prefix(TransferrableObject __instance, DropZone zoneReleased, GameObject releasingHand)
        {
            if ( !__instance.InLeftHand() && Plugin.Instance.lockright)
            {
                return false;
            }
            if (__instance.InLeftHand() && Plugin.Instance.lockleft)
            {
                return false;
            }
            else return true;
        }
    }
}