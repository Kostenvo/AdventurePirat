using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TimeComponent
{
    public class TimerComponent :MonoBehaviour
    {
        [SerializeField] private InvokeTimer[] _timers;

        public void SetTimers(string timerName)
        {
            foreach (var timer in _timers)
            {
                if (timerName.Contains(timer.name))
                {
                    StartCoroutine(StartTimer(timer));
                }
            }
        }

        private IEnumerator StartTimer(InvokeTimer timer)
        {
            yield return new WaitForSeconds(timer.time);
            timer._actionForEndTime.Invoke();
        }

        [Serializable]
        public struct InvokeTimer
        {
            public string name;
            public float time;
            public UnityEvent _actionForEndTime;
        }
    }
}