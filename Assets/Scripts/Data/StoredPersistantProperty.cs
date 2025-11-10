using System.Collections.Generic;
using Subscribe;
using UnityEngine;

namespace Data
{
    public abstract class StoredPersistantProperty<TProperty> : PersistantProperty<TProperty>
    {
       
        private TProperty _storedValue;
        private bool _loaded;
        
        
        

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
                if (EqualityComparer<TProperty>.Default.Equals(_storedValue, value)) return;
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