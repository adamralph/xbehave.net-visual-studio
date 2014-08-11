using System.Collections.Generic;

namespace xBehave.Paster.Gherkin
{
    internal class PlaceHolder
    {
        private ValueTypes _type;
        public string Name { get; private set; }

        public ValueTypes Type
        {
            get
            {
                return _type;
            }
            private set
            {
                if (value > _type)
                    _type = value;
            }
        }

        public PlaceHolder(string name)
        {
            Name = name;
            Values = new List<string>();
        }

        public void AddValue(ExampleValue exampleValue)
        {
            Type = exampleValue.ValueType;
            Values.Add(exampleValue.Value);
        }

        public List<string> Values { get; private set; }
    }

    internal class PlaceHolderCollection
    {
        private readonly List<PlaceHolder> _placeholders = new List<PlaceHolder>();

        public PlaceHolderCollection(IEnumerable<string> exampleNames)
        {
            foreach (var name in exampleNames)
                _placeholders.Add(new PlaceHolder(name));
        }

        public void AddValues(ExampleValue[] examples)
        {
            for (int index = 0; index < _placeholders.Count; index++)
                _placeholders[index].AddValue(examples[index]);
        }

        public IEnumerable<string> CreateExampleAttributes()
        {
            var numberOfAttributes = _placeholders[0].Values.Count;
            for (int index = 0; index < numberOfAttributes; index++)
            {
                
            }
        }
    }
}