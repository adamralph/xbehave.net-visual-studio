namespace xBehave.Paster.Gherkin
{
    internal class Substitution
    {
        public string PlaceHolder { get; private set; }
        public string NewValue { get; private set; }

        public Substitution(string placeHolder, string newValue)
        {
            PlaceHolder = placeHolder;
            NewValue = newValue;
        }
    }
}