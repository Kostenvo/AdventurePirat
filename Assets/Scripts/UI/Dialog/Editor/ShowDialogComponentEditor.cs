using System;
using UnityEditor;

namespace UI.Dialog.Editor
{
    [CustomEditor(typeof(ShowDialogComponent))]
    public class ShowDialogComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _showDialog;

        private void OnEnable()
        {
            _showDialog = serializedObject.FindProperty("_dialogType");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_showDialog);
           // base.OnInspectorGUI();
            var enumNames = _showDialog.enumNames;
            var currentEnumString = enumNames[_showDialog.enumValueIndex];
            var isGettingEnum = Enum.TryParse<DialogType>(currentEnumString, out DialogType currentEnum);
            if (isGettingEnum)
            {
                switch (currentEnum)
                {
                    case  DialogType.Dialog :
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dialogData"));
                        break;
                    case DialogType.Def :
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dialogDef"));
                        break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}