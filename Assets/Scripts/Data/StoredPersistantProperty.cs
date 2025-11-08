using Subscribe;
using UnityEngine;

namespace Data
{
    public abstract class StoredPersistantProperty<TProperty> : PersistantProperty<TProperty>
    {
       
        private TProperty _storedValue;
        private bool _loaded;
        
        
        public override ActionDisposable SubscribeAndInvoke(OnValueChanged onValueChanged)
        {
            ValueChanged += onValueChanged;
            onValueChanged.Invoke(_value, _storedValue);
            return new ActionDisposable(() => ValueChanged -= onValueChanged);
        }

        public override TProperty Value
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
                if (!_loaded)
                {
                    _storedValue = _value = value;
                    _loaded = true;
                }
                if (_storedValue.Equals(value)) return;
                var oldValue = _storedValue;
                _value = _storedValue = value;
                SetValue(_value);
                InvokeChange(_value, oldValue);
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