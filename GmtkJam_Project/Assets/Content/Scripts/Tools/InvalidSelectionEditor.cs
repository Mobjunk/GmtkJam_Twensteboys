using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(InvalidSelection))]
public class InvalidSelectionEditor : Editor
{
    private void OnEnable()
    {
        GameObject selectedGO = Selection.activeGameObject;
        SceneView sceneView = EditorWindow.focusedWindow as SceneView;
        if (selectedGO.transform.parent != null && sceneView != null)
        {
            Selection.activeGameObject = selectedGO.transform.parent.gameObject;
            return;
        }
    }
}
#endif