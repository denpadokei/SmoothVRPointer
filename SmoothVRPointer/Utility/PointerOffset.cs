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
        public static void Offset(XRNode node, ref Vector3 pos, ref Quaternion rot)
        {
            switch (node) {
                case XRNode.LeftHand:
                    if (beforeLeftTranceform == null) {
                        var go = new GameObject();
                        GameObject.DontDestroyOnLoad(go);
                        beforeLeftTranceform = go.transform;
                        return;
                    }
                    pos = Vector3.Lerp(beforeLeftTranceform.position, pos, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    rot = Quaternion.Slerp(beforeLeftTranceform.rotation, rot, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    //transform.SetPositionAndRotation(tmpPosition, tmpRotation);
                    beforeLeftTranceform.SetPositionAndRotation(pos, rot);
                    break;
                case XRNode.RightHand:
                    if (beforeRightTranceform == null) {
                        var go = new GameObject();
                        GameObject.DontDestroyOnLoad(go);
                        beforeRightTranceform = go.transform;
                        return;
                    }
                    pos = Vector3.Lerp(beforeRightTranceform.position, pos, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    rot = Quaternion.Slerp(beforeRightTranceform.rotation, rot, Time.deltaTime * PluginConfig.Instance.OffsetValue);
                    //transform.SetPositionAndRotation(tmpPosition, tmpRotation);
                    beforeRightTranceform.SetPositionAndRotation(pos, rot);
                    break;
                default:
                    return;
            }
        }
        private static Transform beforeRightTranceform;
        private static Transform beforeLeftTranceform;
    }
}
