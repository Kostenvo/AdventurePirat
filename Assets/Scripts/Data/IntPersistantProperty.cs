namespace Data
{
    [System.Serializable]
    public class IntPersistantProperty : PersistantProperty<int>
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