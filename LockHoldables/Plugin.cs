using BepInEx;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace LockHoldables
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static volatile Plugin Instance;
        bool toggll;
        bool togglr;
        public bool lockleft;
        public bool lockright;
        bool lcooldown;
        bool rcooldown;

        private readonly XRNode rNode = XRNode.RightHand;
        private readonly XRNode lNode = XRNode.LeftHand;
        void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Instance = this;
        }

        void Update()
        {
            InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out togglr);
            InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out toggll);
            if (togglr && rcooldown == false)
            {
                rcooldown = true;
                lockright = !lockright;
                StartCoroutine(LR(1));
            }
            if (toggll && lcooldown == false)
            {
                lcooldown = true;
                lockleft = !lockleft;
                StartCoroutine(LL(1));
            }
        }
        IEnumerator LL(float sec)
        {
            yield return new WaitForSeconds(sec);
            lcooldown = false;
        }
        IEnumerator LR(float sec)
        {
            yield return new WaitForSeconds(sec);
            rcooldown = false;
        }
    }
}
