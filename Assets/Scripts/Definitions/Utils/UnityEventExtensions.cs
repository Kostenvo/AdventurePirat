using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public static class UnityEventExtensions
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;

        /// <summary>
        /// Извлекает все GameObject'ы, которые являются целями для вызовов в UnityEvent
        /// </summary>
        /// <param name="unityEvent">UnityEvent для анализа</param>
        /// <returns>Массив GameObject'ов, которые являются целями событий</returns>
        public static GameObject[] ToGameObjects(this UnityEvent unityEvent)
        {
            var gos = new List<GameObject>();

            if (unityEvent == null) return gos.ToArray();

            // Шаг 1: Получаем m_PersistentCalls из UnityEventBase
            var eventType = typeof(UnityEventBase);
            var persistentCallsField = eventType.GetField("m_PersistentCalls", BindingAttr);
            if (persistentCallsField == null) return gos.ToArray();

            var persistentCallsValue = persistentCallsField.GetValue(unityEvent);

            // Шаг 2: Получаем m_Calls из PersistentCalls
            var callGroupType = persistentCallsValue.GetType();
            var callGroupField = callGroupType.GetField("m_Calls", BindingAttr);
            if (callGroupField == null) return gos.ToArray();

            var callGroupValue = (IEnumerable)callGroupField.GetValue(persistentCallsValue);

            // Шаг 3: Проверяем что это действительно List<PersistentCall>
            var listType = callGroupField.GetValue(persistentCallsValue).GetType();
            if (!listType.IsGenericType || listType.GetGenericTypeDefinition() != typeof(List<>))
                return gos.ToArray();

            var itemType = listType.GetGenericArguments().Single();

            // Шаг 4: Проходим по каждому PersistentCall
            foreach (var pc in callGroupValue)
            {
                // Получаем m_Target из PersistentCall
                var itemField = itemType.GetField("m_Target", BindingAttr);
                if (itemField == null) continue;

                var itemValue = (Object)itemField.GetValue(pc);
                if (itemValue == null) continue;

                // Получаем gameObject из UnityEngine.Object
                var propertyInfo = itemValue.GetType().GetProperty("gameObject");
                if (propertyInfo == null) continue;

                var go = (GameObject)propertyInfo.GetValue(itemValue);
                if (go != null)
                {
                    gos.Add(go);
                }
            }

            return gos.ToArray();
        }
    }
}
