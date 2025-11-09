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
            _showDialog = serializedObject.FindProperty("_sentenceType");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_showDialog);
           // base.OnInspectorGUI();
            var enumNames = _showDialog.enumNames;
            var currentEnumString = enumNames[_showDialog.enumValueIndex];
            var isGettingEnum = Enum.TryParse<SentenceType>(currentEnumString, out SentenceType currentEnum);
            if (isGettingEnum)
            {
                switch (currentEnum)
                {
                    case  SentenceType.Dialog :
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dialogData"));
                        break;
                    case SentenceType.Def :
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dialogDef"));
                        break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}