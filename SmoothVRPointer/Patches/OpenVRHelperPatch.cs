using HarmonyLib;
using SmoothVRPointer.Configuration;
using SmoothVRPointer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace SmoothVRPointer.Patches
{
    [HarmonyPatch(
        typeof(OpenVRHelper),
        "AdjustControllerTransform",
        new Type[] { typeof(XRNode), typeof(Transform), typeof(Vector3), typeof(Vector3) })]
    public class OpenVRHelperPatch
    {
        public static void Prefix(ref XRNode node, ref Transform transform, ref Vector3 position, ref Vector3 rotation)
        {
            if (!PluginConfig.Instance.Enable) {
                return;
            }
            if (CurrentSceneName == "GameCore") {
                return;
            }
            PointerOffset.Offset(node, transform);
        }
        public static string CurrentSceneName { get; set; }
    }
}
