namespace Data
{
    public class StringPersistantProperty : PersistantProperty<string>
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