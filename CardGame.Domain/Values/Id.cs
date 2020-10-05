using CardGame.Domain.Resources;
using CardGame.Framework;
using System;

namespace CardGame.Domain.Values
{
    public class Id : Value<Id>
    {
        Guid Value { get; }

        public Id(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), Strings.InvalidCardId);

            Value = value;
        }
    }
}
