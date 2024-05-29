using HarmonyLib;
using SmoothVRPointer.Configuration;
using SmoothVRPointer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace SmoothVRPointer.Patches
{
    [HarmonyPatch]
    public class IVRPlatformHelperPatch
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            var hmLibPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location)), "Beat Saber_Data", "Managed", "HMLib.dll");
            Plugin.Log.Debug(hmLibPath);
            var hmLib = Assembly.LoadFrom(hmLibPath);
            if (hmLib == null)
            {
                yield break;
            }
            foreach (var type in hmLib.GetTypes().Where(x => typeof(IVRPlatformHelper).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract && !x.IsInterface))
            {
                yield return type.GetMethod(nameof(IVRPlatformHelper.GetNodePose), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }

        //public static void Prefix(ref XRNode node, ref Transform transform, ref Vector3 position, ref Vector3 rotation)
        public static void Postfix(XRNode nodeType, ref Vector3 pos, ref Quaternion rot)
        {
            if (!PluginConfig.Instance.Enable || string.Equals(CurrentSceneName, "GameCore", StringComparison.Ordinal))
            {
                return;
            }
            if (nodeType == XRNode.LeftHand || nodeType == XRNode.RightHand)
            {
                PointerOffset.Offset(nodeType, ref pos, ref rot);
            }
        }
        public static string CurrentSceneName { get; set; }
    }
}
