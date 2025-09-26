using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FolderAppearance
{
#if UNITY_EDITOR
    public static class FolderAppearanceExporter
    {
        private static readonly string[] k_Paths = new[]
        {
            "Assets/Editor/Scriptes/FolderAppearanceSettings.cs",
            "Assets/Editor/Scriptes/FolderAppearanceDrawer.cs",
            "Assets/Editor/Scriptes/FolderAppearanceWindow.cs",
            "Assets/Editor/Scriptes/FolderAppearanceExporter.cs",
        };

        [MenuItem("Tools/Folder Appearance/Export UnityPackage...")]
        public static void ExportPackage()
        {
            var defaultName = "FolderAppearancePlugin.unitypackage";
            var savePath = EditorUtility.SaveFilePanel("Export Folder Appearance Plugin", Application.dataPath, defaultName, "unitypackage");
            if (string.IsNullOrEmpty(savePath)) return;

            // Ensure scripts exist (in case structure differed)
            var existing = System.Array.FindAll(k_Paths, AssetExists);
            if (existing.Length == 0)
            {
                EditorUtility.DisplayDialog("Export Plugin", "No plugin files found at expected paths.", "OK");
                return;
            }

            AssetDatabase.ExportPackage(existing, savePath, ExportPackageOptions.Recurse | ExportPackageOptions.Interactive | ExportPackageOptions.IncludeDependencies);
            EditorUtility.DisplayDialog("Export Plugin", "UnityPackage exported:\n" + savePath, "OK");
        }

        private static bool AssetExists(string assetPath)
        {
            var obj = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            return obj != null || System.IO.File.Exists(assetPath) || AssetDatabase.IsValidFolder(assetPath);
        }
    }
#endif
}


