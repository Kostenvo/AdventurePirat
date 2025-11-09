using Subscribe;
using UnityEngine;

namespace UI.Localization
{
    public abstract class BaseLocalize: MonoBehaviour
    {
        ComposideDisposible _trash = new ComposideDisposible();

        protected virtual void Start()
        {
            _trash.Retain( LocalizationManager.Instance.CurrentLocale.SubscribeAndInvoke((_,_)=>ChangeLocale()));
        }

        protected abstract void ChangeLocale();
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}