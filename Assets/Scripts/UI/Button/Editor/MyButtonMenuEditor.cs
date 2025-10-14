using UnityEditor;
using UnityEditor.UI;

namespace UI.Button.Editor
{
    [CustomEditor(typeof(MyButtonMenu), true)]
    [CanEditMultipleObjects]
    public class MyButtonMenuEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_normalText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_pressedText"));
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}