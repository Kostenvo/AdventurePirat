using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data
{
    [Serializable]
    public class StoredFloatPersistentProperty : PersistantProperty<float>
    {
        [SerializeField] private string _storageName;


        protected override void SetValue(float value)
        {
            PlayerPrefs.SetFloat(_storageName, value);
            PlayerPrefs.Save();
        }

        protected override float GetValue()
        {
            return PlayerPrefs.GetFloat(_storageName, 0);
        }

       
    }

    public enum SoundType
    {
        Music,
        SFX
    }
}