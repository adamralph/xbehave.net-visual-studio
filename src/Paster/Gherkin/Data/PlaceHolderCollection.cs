using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        protected PlaceHolderCollection()
        {}

        public static PlaceHolderCollection Empty
        {
            get
            {
                return new PlaceHolderCollection();
            }
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
                var currentIndex = index;
                var valueString = String.Join(",", _placeholders.Select(p =>
                                                                            {
                                                                                if (p.Type == ValueTypes.String)
                                                                                {
                                                                                    return String.Format("\"{0}\"", p.Values[currentIndex]);
                                                                                }
                                                                                return p.Values[currentIndex];
                                                                            }));
                yield return String.Format("[Example({0})]", valueString);
            }
        }

        public bool Any()
        {
            return _placeholders.Any();
        }

        public IEnumerable<string> CreateParameters()
        {
            return _placeholders.Select(placeholder => String.Format("{0} {1}",
                                                                     placeholder.Type.ToString()
                                                                                .ToLower(CultureInfo.CurrentUICulture),
                                                                     placeholder.Name));
        }

        private const string PlaceHolderFormat = @"<{0}>";
        private const string SubstituionFormat = @"{{{0}}}";

        public Substitution[] CreateSubstitutions()
        {
            return _placeholders.Select((placeholder, index) =>
                                            {
                                                var name = String.Format(PlaceHolderFormat, placeholder.Name);
                                                var sub = String.Format(SubstituionFormat, index.ToString(CultureInfo.CurrentUICulture));
                                                return new Substitution(name, sub);
                                            })
                                .ToArray();
        }
    }
}