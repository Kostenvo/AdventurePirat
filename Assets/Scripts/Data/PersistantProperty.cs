using Subscribe;
using UnityEngine;

namespace Data
{
    public class PersistantProperty<TProperty>
    {
        [SerializeField] protected TProperty _value;
        
        public  delegate void OnValueChanged(TProperty newValue, TProperty oldValue);

        public event OnValueChanged ValueChanged;  
        
        public ActionDisposable Subscribe(OnValueChanged onValueChanged)
        {
            ValueChanged += onValueChanged;
            return new ActionDisposable(() => ValueChanged -= onValueChanged);
        }
        public virtual ActionDisposable SubscribeAndInvoke(OnValueChanged onValueChanged)
        {
            ValueChanged += onValueChanged;
            onValueChanged.Invoke(_value, _value);
            return new ActionDisposable(() => ValueChanged -= onValueChanged);
        }
        
        
        public virtual TProperty Value
        {
            get
            {
                return _value;
            }
            set
            {
                var oldValue = _value;
                _value  = value;
                ValueChanged?.Invoke(_value, oldValue);
            }
        }

        protected virtual void InvokeChange(TProperty newValue, TProperty oldValue)
        {
            ValueChanged?.Invoke(_value, oldValue);
        }
    }
}