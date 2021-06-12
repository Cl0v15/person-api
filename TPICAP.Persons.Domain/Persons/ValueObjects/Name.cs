using System.Collections.Generic;
using TPICAP.Persons.Domain.Core;
using TPICAP.Persons.Shared;

namespace TPICAP.Persons.Domain.Persons
{
    public class Name : ValueObject
    {
        private const int MaxLength = 50;

        public string Value { get; private set; }

        private Name() { }

        public static Name New(string value)
        {
            value.NotNullOrEmpty();
            value.MaxLength(MaxLength);

            return new Name
            {
                Value = value
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}