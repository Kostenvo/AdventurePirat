#if UNITY_EDITOR
using System;
using Core;
using UnityEditor;
using UnityEngine;

// ВАЖНО:
// - Этот Drawer работает только в Unity Editor и не попадает в билды.
// - Он улучшает UX для полей, помеченных атрибутом
//   [EditorTools.Interface(typeof(IMyInterface))], где тип поля — UnityEngine.Object.
//   Drawer гарантирует, что назначаемые объекты реализуют нужный интерфейс, и
//   показывает понятные подсказки в инспекторе.

namespace EditorTools
{
    /// <summary>
    /// PropertyDrawer для <see cref="InterfaceAttribute"/>.
    /// Рисует ObjectField, который принимает:
    /// - Компоненты, реализующие требуемый интерфейс
    /// - ScriptableObject, реализующие требуемый интерфейс
    /// - GameObject (в этом случае автоматически выбирается первый компонент на
    ///   этом объекте, реализующий интерфейс)
    ///
    /// Если назначение невалидно, Drawer либо игнорирует его, либо показывает
    /// подсказку (HelpBox) с описанием ожиданий.
    /// </summary>
    [CustomPropertyDrawer(typeof(InterfaceAttribute))]
    public class InterfacePropertyDrawer : PropertyDrawer
    {
        private const float HelpBoxHeight = 34f;
        private bool _isInvalid;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var ifaceAttr = (InterfaceAttribute)attribute;
            var expectedInterface = ifaceAttr.InterfaceType;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.HelpBox(position,
                    $"[Interface] can only be used on Object fields. Field '{property.displayName}' is {property.propertyType}.",
                    MessageType.Error);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            // Вычисляем области: основное поле + (опционально) зона HelpBox.
            var fieldRect = position;
            if (_isInvalid)
                fieldRect.height -= HelpBoxHeight + 2f;

            EditorGUI.BeginChangeCheck();
            var picked = EditorGUI.ObjectField(
                fieldRect,
                label,
                property.objectReferenceValue,
                typeof(UnityEngine.Object),
                ifaceAttr.AllowSceneObjects
            );
            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = FilterToInterfaceObject(picked, expectedInterface);
                property.serializedObject.ApplyModifiedProperties();
            }

            // Валидируем текущую ссылку и показываем подсказку при ошибках.
            _isInvalid = !IsValid(property.objectReferenceValue, expectedInterface);
            if (_isInvalid)
            {
                var helpRect = new Rect(
                    position.x,
                    position.y + position.height - (HelpBoxHeight),
                    position.width,
                    HelpBoxHeight
                );
                EditorGUI.HelpBox(helpRect,
                    $"Назначенный объект должен реализовывать интерфейс: {expectedInterface.Name}.\n" +
                    "Бросьте Component/ScriptableObject с этим интерфейсом или GameObject, содержащий такой компонент.",
                    MessageType.Error);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var baseH = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            return baseH + (_isInvalid ? HelpBoxHeight : 0f);
        }

        private static UnityEngine.Object FilterToInterfaceObject(UnityEngine.Object candidate, Type expectedInterface)
        {
            if (candidate == null) return null;

            // Если бросили GameObject — попробуем найти на нём подходящий Component.
            if (candidate is GameObject go)
            {
                var comps = go.GetComponents<Component>();
                foreach (var c in comps)
                {
                    if (c && expectedInterface.IsAssignableFrom(c.GetType()))
                        return c; // выбираем первый подходящий компонент
                }

                return null; // на GameObject не нашли подходящий компонент
            }

            // Если это Component — проверим, реализует ли интерфейс
            if (candidate is Component comp)
                return expectedInterface.IsAssignableFrom(comp.GetType()) ? comp : null;

            // Если это ScriptableObject — проверим, реализует ли интерфейс
            if (candidate is ScriptableObject so)
                return expectedInterface.IsAssignableFrom(so.GetType()) ? so : null;

            // Иные типы не поддерживаются
            return null;
        }

        private static bool IsValid(UnityEngine.Object obj, Type expectedInterface)
        {
            if (obj == null) return true; // пустое поле считаем валидным
            return expectedInterface.IsAssignableFrom(obj.GetType());
        }
    }
}
#endif