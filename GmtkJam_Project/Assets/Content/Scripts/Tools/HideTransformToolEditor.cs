using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(HideTransformTool))]
public class HideTransformToolEditor : Editor
{
    private void OnEnable()
    {
        Tools.hidden = true;
    }

    private void OnDisable()
    {
        Tools.hidden = false;
    }
}
#endif