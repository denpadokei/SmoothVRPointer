using SmoothVRPointer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace SmoothVRPointer.Utility
{
    static class PointerOffset
    {
        public static void Offset(XRNode node, Transform transform)
        {
            Vector3 tmpPosition = Vector3.zero;
            Quaternion tmpRotation = Quaternion.identity;
            switch (node) {
                case XRNode.LeftHand:
                    if (beforeLeftTranceform == null) {
                        var go = new GameObject();
                        GameObject.DontDestroyOnLoad(go);
                        beforeLeftTranceform = go.transform;
                        return;
                    }
                    tmpPosition = Vector3.Lerp(beforeLeftTranceform.position, transform.position, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    tmpRotation = Quaternion.Slerp(beforeLeftTranceform.rotation, transform.rotation, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    transform.SetPositionAndRotation(tmpPosition, tmpRotation);
                    beforeLeftTranceform.SetPositionAndRotation(transform.position, transform.rotation);
                    break;
                case XRNode.RightHand:
                    if (beforeRightTranceform == null) {
                        var go = new GameObject();
                        GameObject.DontDestroyOnLoad(go);
                        beforeRightTranceform = go.transform;
                        return;
                    }
                    tmpPosition = Vector3.Lerp(beforeRightTranceform.position, transform.position, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    tmpRotation = Quaternion.Slerp(beforeRightTranceform.rotation, transform.rotation, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    transform.SetPositionAndRotation(tmpPosition, tmpRotation);
                    beforeRightTranceform.SetPositionAndRotation(transform.position, transform.rotation);
                    break;
                default:
                    return;
            }
        }
        private static Transform beforeRightTranceform;
        private static Transform beforeLeftTranceform;
    }
}
