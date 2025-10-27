using UnityEngine;

namespace Data
{
    public class StoredStringPersistentProperty : PersistantProperty<string>
    {
        [SerializeField] private string storedKey;
        protected override void SetValue(string value)
        {
            PlayerPrefs.SetString(storedKey, value);
            PlayerPrefs.Save();
        }

        protected override string GetValue()
        {
            return PlayerPrefs.GetString(storedKey);
        }
    }
}