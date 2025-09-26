using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FolderAppearance
{
#if UNITY_EDITOR
    public class FolderAppearanceWindow : EditorWindow
    {
        private FolderAppearanceSettings settings;
        private string selectedGuid;
        private string selectedPath;

        private Color newColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
        private Texture2D newIcon;
        private bool useColor;
        private bool useIcon;

        [MenuItem("Tools/Folder Appearance/Editor" )]
        public static void Open()
        {
            var wnd = GetWindow<FolderAppearanceWindow>(false, "Folder Appearance", true);
            wnd.minSize = new Vector2(360, 240);
            wnd.Show();
        }

        [MenuItem("Assets/Folder Appearance/Customize Folder", true)]
        private static bool ValidateCustomize()
        {
            var active = Selection.activeObject;
            if (active == null) return false;
            var path = AssetDatabase.GetAssetPath(active);
            return AssetDatabase.IsValidFolder(path);
        }

        [MenuItem("Assets/Folder Appearance/Customize Folder")] 
        private static void CustomizeFromContext()
        {
            Open();
        }

        private void OnEnable()
        {
            settings = FolderAppearanceSettings.LoadOrCreateSettings();
            RefreshSelection();
            Selection.selectionChanged += RefreshSelection;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= RefreshSelection;
        }

        private void RefreshSelection()
        {
            selectedGuid = null;
            selectedPath = null;
            var active = Selection.activeObject;
            if (active == null) return;
            var path = AssetDatabase.GetAssetPath(active);
            if (!AssetDatabase.IsValidFolder(path)) return;
            selectedPath = path;
            selectedGuid = AssetDatabase.AssetPathToGUID(path);

            if (settings.TryGetRule(selectedGuid, out var rule))
            {
                newColor = rule.color;
                newIcon = rule.icon;
                useColor = rule.useCustomColor;
                useIcon = rule.useCustomIcon;
            }
            else
            {
                useColor = false;
                useIcon = false;
                newIcon = null;
                newColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
            }
            Repaint();
        }

        private void OnGUI()
        {
            if (settings == null)
            {
                settings = FolderAppearanceSettings.LoadOrCreateSettings();
            }

            EditorGUILayout.LabelField("Folder Appearance", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(string.IsNullOrEmpty(selectedGuid)))
            {
                EditorGUILayout.LabelField("Selected Folder:", selectedPath ?? "<none>");
                EditorGUILayout.Space();

                useColor = EditorGUILayout.ToggleLeft("Use Custom Color", useColor);
                if (useColor)
                {
                    newColor = EditorGUILayout.ColorField("Color", newColor);
                }

                EditorGUILayout.Space();

                useIcon = EditorGUILayout.ToggleLeft("Use Custom Icon", useIcon);
                if (useIcon)
                {
                    newIcon = (Texture2D)EditorGUILayout.ObjectField("Icon", newIcon, typeof(Texture2D), false);
                }

                EditorGUILayout.Space();

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Apply", GUILayout.Height(28)))
                    {
                        ApplyChanges();
                    }
                    if (GUILayout.Button("Clear", GUILayout.Height(28)))
                    {
                        ClearRule();
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Defaults", EditorStyles.boldLabel);
            settings.enableDefaultColors = EditorGUILayout.ToggleLeft("Enable Default Colors", settings.enableDefaultColors);
            settings.defaultColorAlpha = EditorGUILayout.Slider("Default Color Alpha", settings.defaultColorAlpha, 0f, 1f);
            settings.useGradientDefaults = EditorGUILayout.ToggleLeft("Use Gradient Defaults", settings.useGradientDefaults);
            if (settings.useGradientDefaults)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    settings.hueStart = EditorGUILayout.Slider("Hue Start", settings.hueStart, 0f, 1f);
                    settings.hueEnd = EditorGUILayout.Slider("Hue End", settings.hueEnd, 0f, 1f);
                    settings.gradientSaturation = EditorGUILayout.Slider("Saturation", settings.gradientSaturation, 0f, 1f);
                    settings.gradientValue = EditorGUILayout.Slider("Value", settings.gradientValue, 0f, 1f);
                    settings.varyByDepth = EditorGUILayout.ToggleLeft("Vary By Depth", settings.varyByDepth);
                    if (settings.varyByDepth)
                    {
                        settings.depthSaturationStep = EditorGUILayout.Slider("Depth Saturation Step", settings.depthSaturationStep, -0.5f, 0.5f);
                        settings.depthValueStep = EditorGUILayout.Slider("Depth Value Step", settings.depthValueStep, -0.5f, 0.5f);
                    }
                }
            }
            settings.autoContrastText = EditorGUILayout.ToggleLeft("Auto Contrast Text (legacy)", settings.autoContrastText);
            settings.alwaysDrawLabelOnTop = EditorGUILayout.ToggleLeft("Always Draw Label On Top", settings.alwaysDrawLabelOnTop);
            settings.overrideLabelColor = EditorGUILayout.ColorField("Label Color", settings.overrideLabelColor);
            settings.drawLabelShadow = EditorGUILayout.ToggleLeft("Draw Label Shadow", settings.drawLabelShadow);
            settings.labelFontStyle = (FontStyle)EditorGUILayout.EnumPopup("Label Font Style", settings.labelFontStyle);
            settings.labelFontSize = EditorGUILayout.IntSlider("Label Font Size", settings.labelFontSize, 0, 24);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Icon Size", EditorStyles.boldLabel);
            settings.listIconScale = EditorGUILayout.Slider("List Icon Scale", settings.listIconScale, 0.5f, 3f);
            settings.gridIconScale = EditorGUILayout.Slider("Grid Icon Scale", settings.gridIconScale, 0.5f, 3f);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Initials (no icon)", EditorStyles.boldLabel);
            settings.drawInitialsWhenNoIcon = EditorGUILayout.ToggleLeft("Draw Initials When No Icon", settings.drawInitialsWhenNoIcon);
            if (settings.drawInitialsWhenNoIcon)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    settings.initialsColor = EditorGUILayout.ColorField("Initials Color", settings.initialsColor);
                    settings.initialsFontSize = EditorGUILayout.IntSlider("Initials Font Size", settings.initialsFontSize, 8, 40);
                    settings.initialsFontStyle = (FontStyle)EditorGUILayout.EnumPopup("Initials Font Style", settings.initialsFontStyle);
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(settings);
            }
        }

        private void ApplyChanges()
        {
            if (string.IsNullOrEmpty(selectedGuid)) return;
            var rule = settings.GetOrCreateRule(selectedGuid);
            rule.useCustomColor = useColor;
            rule.color = newColor;
            rule.useCustomIcon = useIcon;
            rule.icon = newIcon;
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            EditorApplication.RepaintProjectWindow();
        }

        private void ClearRule()
        {
            if (string.IsNullOrEmpty(selectedGuid)) return;
            settings.RemoveRule(selectedGuid);
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            EditorApplication.RepaintProjectWindow();
            RefreshSelection();
        }
    }
#endif
}


