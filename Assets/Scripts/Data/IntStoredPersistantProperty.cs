namespace Data
{
    [System.Serializable]
    public class IntStoredPersistantProperty : StoredPersistantProperty<int>
    {
        protected override void SetValue(int value)
        {

        }

        protected override int GetValue()
        {
            return _value;
        }
    }
}