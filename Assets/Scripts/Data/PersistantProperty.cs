using System;
using Subscribe;
using UnityEngine;

namespace Data
{
    public abstract class PersistantProperty<TProperty>
    {
        [SerializeField] protected TProperty _value;
        private TProperty _storedValue;
        private bool _loaded;

        public delegate void OnValueChanged(TProperty newValue, TProperty oldValue);

        public event OnValueChanged ValueChanged;

        public ActionDisposable Subscribe(OnValueChanged onValueChanged)
        {
            ValueChanged += onValueChanged;
            return new ActionDisposable(() => ValueChanged -= onValueChanged);
        }
        public ActionDisposable SubscribeAndInvoke(OnValueChanged onValueChanged)
        {
            ValueChanged += onValueChanged;
            onValueChanged.Invoke(_value, _storedValue);
            return new ActionDisposable(() => ValueChanged -= onValueChanged);
        }


        public TProperty Value
        {
            get
            {
                if (!_loaded)
                {
                    _storedValue = _value = GetValue();
                    _loaded = true;
                }

                return _storedValue;
            }
            set
            {
                if (_storedValue.Equals(value)) return;
                var oldValue = _storedValue;
                _value = _storedValue = value;
                SetValue(_value);
                ValueChanged?.Invoke(_value, oldValue);
            }
        }
        public void Validate()
        {
            Value = _value;
        }

        protected abstract void SetValue(TProperty value);
        protected abstract TProperty GetValue();
    }
}