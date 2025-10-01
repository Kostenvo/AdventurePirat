using System;
using UnityEngine;

// ПРИМЕЧАНИЕ:
// Этот атрибут должен находиться вне папки Editor, чтобы быть доступным
// для рантайм-скриптов и сборки. Соответствующий PropertyDrawer остаётся
// в папке Editor и не попадает в билд.

namespace EditorTools
{
    /// <summary>
    /// Атрибут для пометки сериализуемого поля UnityEngine.Object, чтобы
    /// пользовательский PropertyDrawer мог требовать, чтобы назначаемые объекты
    /// реализовывали указанный интерфейс.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InterfaceAttribute : PropertyAttribute
    {
        /// <summary>
        /// Тип интерфейса, который должны реализовывать назначаемые объекты.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Разрешать ли объекты из сцены (true) или ограничиться только ассетами (false).
        /// Флаг передаётся в EditorGUI.ObjectField для управления выбором объектов.
        /// </summary>
        public bool AllowSceneObjects { get; }

        /// <param name="interfaceType">Тип интерфейса, который должен быть реализован.</param>
        /// <param name="allowSceneObjects">Разрешить выбор объектов сцены.</param>
        public InterfaceAttribute(Type interfaceType, bool allowSceneObjects = true)
        {
            InterfaceType = interfaceType;
            AllowSceneObjects = allowSceneObjects;
        }
    }
}


