using HarmonyLib;
using LockHoldables;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static TransferrableObject;

namespace Lock__instances.Patches
{
    [HarmonyPatch(typeof(TransferrableObject))]
    [HarmonyPatch("OnRelease")]
    [HarmonyPatch(new Type[] { typeof(DropZone), typeof(GameObject)})]
    internal class HoldPatch
    {
        private static bool Prefix(TransferrableObject __instance, DropZone zoneReleased, GameObject releasingHand)
        {
            if ( !__instance.InLeftHand() && Plugin.Instance.lockright)
            {
                __instance.gripInteractor.myCollider.enabled = false;
                __instance.gripInteractor.enabled = false;
                return false;
            }
            if (__instance.InLeftHand() && Plugin.Instance.lockleft)
            {
                __instance.gripInteractor.myCollider.enabled = false;
                __instance.gripInteractor.enabled = false;
                return false;
            }
            else
            {
                __instance.gripInteractor.myCollider.enabled = true;
                __instance.gripInteractor.enabled = true;
                return true;
            }
        }
    }
}