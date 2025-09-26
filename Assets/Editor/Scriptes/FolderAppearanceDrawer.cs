using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.IMGUI.Controls;
#endif

namespace FolderAppearance
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class FolderAppearanceDrawer
    {
        private static readonly Type projectBrowserType;

        static FolderAppearanceDrawer()
        {
            projectBrowserType = typeof(Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            if (string.IsNullOrEmpty(guid)) return;

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) return;

            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            if (asset == null) return;

            // Only folders
            if (!AssetDatabase.IsValidFolder(path)) return;

            var settings = FolderAppearanceSettings.LoadOrCreateSettings();

            // Determine color
            Color? backgroundColor = null;
            Texture2D customIcon = null;

            if (settings.TryGetRule(guid, out var rule))
            {
                if (rule.useCustomColor)
                {
                    backgroundColor = rule.color;
                }
                if (rule.useCustomIcon)
                {
                    customIcon = rule.icon;
                }
            }

            if (!backgroundColor.HasValue && settings.enableDefaultColors)
            {
                backgroundColor = GetDefaultColor(path, settings);
            }

            // Draw colored background under the folder label/icon
            if (backgroundColor.HasValue)
            {
                var bg = selectionRect;
                // Slightly shrink to fit nicely
                bg.xMin += 1f;
                bg.xMax -= 1f;
                bg.yMin += 1f;
                bg.yMax -= 1f;
                EditorGUI.DrawRect(bg, backgroundColor.Value);
            }

            // Draw custom icon overlay (left of the label in list view or over the preview in grid)
            if (customIcon != null)
            {
                var iconRect = GetIconRect(selectionRect);
                // Hide default Unity folder icon under our custom image
                var rowBg = GetProjectRowBackgroundColor();
                EditorGUI.DrawRect(iconRect, rowBg);
                GUI.DrawTexture(iconRect, customIcon, ScaleMode.ScaleToFit, true);
            }
            else if (settings.drawInitialsWhenNoIcon)
            {
                var iconRect = GetIconRect(selectionRect);
                var initials = GetInitials(System.IO.Path.GetFileName(path));
                if (!string.IsNullOrEmpty(initials))
                {
                    var style = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fontStyle = settings.initialsFontStyle,
                        fontSize = settings.initialsFontSize
                    };
                    var shadowColor = new Color(0f, 0f, 0f, 0.6f);
                    var textColor = settings.initialsColor;
                    // Shadow
                    var shadowRect = new Rect(iconRect.x + 1f, iconRect.y + 1f, iconRect.width, iconRect.height);
                    var shadowStyle = new GUIStyle(style);
                    shadowStyle.normal.textColor = shadowColor;
                    GUI.Label(shadowRect, initials, shadowStyle);
                    // Text
                    style.normal.textColor = textColor;
                    GUI.Label(iconRect, initials, style);
                }
            }

            // Optional: draw our own readable label on top
            if (settings.alwaysDrawLabelOnTop)
            {
                var labelRect = GetLabelRect(selectionRect);
                var name = System.IO.Path.GetFileName(path);

                // Draw a small solid strip to mask what's beneath only under text area
                var bgRect = labelRect;
                bgRect.height = Mathf.Min(bgRect.height, 16f);
                var maskColor = new Color(0f, 0f, 0f, 0.35f);
                EditorGUI.DrawRect(bgRect, maskColor);

                var labelStyle = new GUIStyle(EditorStyles.label)
                {
                    fontStyle = settings.labelFontStyle,
                };
                if (settings.labelFontSize > 0)
                {
                    labelStyle.fontSize = settings.labelFontSize;
                }
                labelStyle.normal.textColor = settings.overrideLabelColor;

                if (settings.drawLabelShadow)
                {
                    var shadow = new Rect(labelRect.x + 1f, labelRect.y + 1f, labelRect.width, labelRect.height);
                    var shadowStyle = new GUIStyle(labelStyle);
                    shadowStyle.normal.textColor = new Color(0f, 0f, 0f, 0.7f);
                    GUI.Label(shadow, name, shadowStyle);
                }

                GUI.Label(labelRect, name, labelStyle);
            }
        }

        private static Color GetProjectRowBackgroundColor()
        {
            // Approximate Project window background based on Editor skin
            if (EditorGUIUtility.isProSkin)
            {
                return new Color(0.219f, 0.219f, 0.219f, 1f); // ~#383838
            }
            else
            {
                return new Color(0.76f, 0.76f, 0.76f, 1f); // light skin
            }
        }

        private static string GetInitials(string folderName)
        {
            if (string.IsNullOrEmpty(folderName)) return string.Empty;
            // Take first two letters/numbers, skip spaces and punctuation
            System.Text.StringBuilder sb = new System.Text.StringBuilder(2);
            for (int i = 0; i < folderName.Length && sb.Length < 2; i++)
            {
                char c = folderName[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToUpperInvariant(c));
                }
            }
            return sb.ToString();
        }

        private static Rect GetIconRect(Rect selectionRect)
        {
            // Heuristic for list vs grid view
            if (selectionRect.height > 20f)
            {
                // Grid view: icon should start from very top, no padding
                var s = FolderAppearanceSettings.LoadOrCreateSettings();
                float baseSize = Mathf.Min(selectionRect.width, selectionRect.height);
                float size = Mathf.Clamp(baseSize * s.gridIconScale, 8f, selectionRect.width);
                return new Rect(selectionRect.x + (selectionRect.width - size) * 0.5f, selectionRect.y, size, size);
            }
            else
            {
                // List view: icon should start from very left, no padding
                var s = FolderAppearanceSettings.LoadOrCreateSettings();
                float baseSize = selectionRect.height;
                float size = Mathf.Clamp(baseSize * s.listIconScale, 8f, selectionRect.height);
                return new Rect(selectionRect.x, selectionRect.y + (selectionRect.height - size) * 0.5f, size, size);
            }
        }

        private static Rect GetLabelRect(Rect selectionRect)
        {
            if (selectionRect.height > 20f)
            {
                // Grid view: label usually below; we mimic by drawing on bottom area
                return new Rect(selectionRect.x + 2f, selectionRect.yMax - 18f, selectionRect.width - 4f, 16f);
            }
            else
            {
                // List view: after icon area
                return new Rect(selectionRect.x + selectionRect.height + 4f, selectionRect.y, selectionRect.width - selectionRect.height - 6f, selectionRect.height);
            }
        }

        private static Color GetDeterministicPastel(string input, float alpha)
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < input.Length; i++)
                {
                    hash = hash * 31 + input[i];
                }
                float h = ((hash & 0xFFFFFF) / (float)0xFFFFFF);
                Color rgb = Color.HSVToRGB(h, 0.25f, 1.0f);
                rgb.a = Mathf.Clamp01(alpha);
                return rgb;
            }
        }

        private static Color GetDefaultColor(string path, FolderAppearanceSettings s)
        {
            if (!s.useGradientDefaults)
            {
                return GetDeterministicPastel(path, s.defaultColorAlpha);
            }

            // Hash for stable hue interpolation between start and end
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < path.Length; i++) hash = hash * 31 + path[i];
                float tHue = ((hash & 0x7FFFFFFF) / (float)int.MaxValue);
                float hue = Mathf.LerpUnclamped(s.hueStart, s.hueEnd, tHue);

                // Depth variation by folder hierarchy depth
                int depth = GetDepth(path);
                float sat = Mathf.Clamp01(s.gradientSaturation + s.depthSaturationStep * depth);
                float val = Mathf.Clamp01(s.gradientValue + s.depthValueStep * depth);
                if (!s.varyByDepth)
                {
                    sat = s.gradientSaturation;
                    val = s.gradientValue;
                }

                Color rgb = Color.HSVToRGB(Mathf.Repeat(hue, 1f), sat, val);
                rgb.a = Mathf.Clamp01(s.defaultColorAlpha);
                return rgb;
            }
        }

        private static int GetDepth(string path)
        {
            // Count '/' after Assets
            int depth = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/') depth++;
            }
            // e.g., Assets = 0, Assets/Sub = 1
            return Mathf.Max(0, depth - 1);
        }
    }
#endif
}


