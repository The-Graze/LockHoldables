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
                __instance.gripInteractor.gameObject.SetActive(false);
                return false;
            }
            if (__instance.InLeftHand() && Plugin.Instance.lockleft)
            {
                __instance.gripInteractor.gameObject.SetActive(false);
                return false;
            }
            else
            {
                __instance.gripInteractor.gameObject.SetActive(true);
                return true;
            }
        }
    }
}