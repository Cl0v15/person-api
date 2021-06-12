using System;
using System.Collections.Generic;
using TPICAP.Persons.Domain.Core;
using TPICAP.Persons.Shared;

namespace TPICAP.Persons.Domain.Persons
{
    public class BirthDate : ValueObject
    {
        public DateTime Value { get; private set; }

        private BirthDate() { }

        public static BirthDate New(DateTime value)
        {
            value.Must(v => v < DateTime.Now, $"{nameof(value)} must be older than today.");

            return new BirthDate
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