namespace Data
{
    public class StringStoredPersistantProperty : StoredPersistantProperty<string>
    {
        protected override void SetValue(string value)
        {

        }

        protected override string GetValue()
        {
            return _value;
        }
    }
}