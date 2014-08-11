using System;

namespace xBehave.Paster.Gherkin
{
    internal class ExampleValue
    {
        public ExampleValue(int index, string value)
        {
            Index = index;
            Value = value;
            ValueType = TypeGuesser.Guess(value);
        }

        public int Index { get; private set; }
        public string Value { get; private set; }
        public ValueTypes ValueType { get; private set; }
    }
}