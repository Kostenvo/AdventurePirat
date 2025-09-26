using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FolderAppearance
{
    [Serializable]
    public class FolderRule
    {
        public string guid;
        public Color color = new Color(0.2f, 0.2f, 0.2f, 0.25f);
        public bool useCustomColor;
        public Texture2D icon;
        public bool useCustomIcon;
    }

    public class FolderAppearanceSettings : ScriptableObject
    {
        public bool enableDefaultColors = true;
        public float defaultColorAlpha = 0.2f;

        [Header("Default Gradient Colors")]
        [Tooltip("Use HSV gradient to color folders by default.")]
        public bool useGradientDefaults = true;
        [Range(0f, 1f)] public float hueStart = 0.0f;
        [Range(0f, 1f)] public float hueEnd = 1.0f;
        [Range(0f, 1f)] public float gradientSaturation = 0.25f;
        [Range(0f, 1f)] public float gradientValue = 1.0f;
        [Tooltip("Vary saturation/value by depth in hierarchy.")]
        public bool varyByDepth = true;
        [Range(-0.5f, 0.5f)] public float depthValueStep = -0.06f;
        [Range(-0.5f, 0.5f)] public float depthSaturationStep = 0.0f;

        [Tooltip("If true, the label text color will switch to white on dark backgrounds.")]
        public bool autoContrastText = false;

        [Header("Label Rendering")]
        [Tooltip("If true, draws a custom bright label text on top to ensure readability.")]
        public bool alwaysDrawLabelOnTop = true;
        public Color overrideLabelColor = Color.white;
        public bool drawLabelShadow = true;
        [Tooltip("Font style for the overlay label text.")]
        public FontStyle labelFontStyle = FontStyle.Bold;
        [Tooltip("Override font size (0 = use Unity default)")]
        [Range(0, 24)] public int labelFontSize = 0;

        [Header("Icon Size")] 
        [Tooltip("Scale of icon in list view (row). Row height is fixed by Unity.")]
        [Range(0.5f, 3f)] public float listIconScale = 1.0f;
        [Tooltip("Scale of icon in grid view (tiles).")]
        [Range(0.5f, 3f)] public float gridIconScale = 2.0f;

        [Header("Initials (when no custom icon)")]
        public bool drawInitialsWhenNoIcon = true;
        public Color initialsColor = new Color(1f, 1f, 1f, 0.95f);
        [Range(8, 40)] public int initialsFontSize = 16;
        public FontStyle initialsFontStyle = FontStyle.Bold;

        [SerializeField]
        private List<FolderRule> rules = new List<FolderRule>();

        public IReadOnlyList<FolderRule> Rules => rules;

        public bool TryGetRule(string guid, out FolderRule rule)
        {
            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i] != null && rules[i].guid == guid)
                {
                    rule = rules[i];
                    return true;
                }
            }
            rule = null;
            return false;
        }

        public FolderRule GetOrCreateRule(string guid)
        {
            if (TryGetRule(guid, out var rule))
            {
                return rule;
            }
            var newRule = new FolderRule { guid = guid };
            rules.Add(newRule);
            return newRule;
        }

        public void RemoveRule(string guid)
        {
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                if (rules[i] != null && rules[i].guid == guid)
                {
                    rules.RemoveAt(i);
                }
            }
        }

#if UNITY_EDITOR
        private const string DefaultAssetPath = "Assets/Editor/Scriptes/FolderAppearanceSettings.asset";

        public static FolderAppearanceSettings LoadOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<FolderAppearanceSettings>(DefaultAssetPath);
            if (settings == null)
            {
                settings = CreateInstance<FolderAppearanceSettings>();
                var dir = System.IO.Path.GetDirectoryName(DefaultAssetPath);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                AssetDatabase.CreateAsset(settings, DefaultAssetPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }
#endif
    }
}


