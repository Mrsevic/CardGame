using CardGame.Domain.Resources;
using CardGame.Framework;
using System;

namespace CardGame.Domain.Values
{
    public class CardNumber : Value<CardNumber>
    {
        public int Value { get; }

        public CardNumber(int value)
        {
            const int maxNumber = 10;

            if (value == default || value > maxNumber)
                throw new ArgumentNullException(nameof(value), Strings.InvalidCardNumber);

            Value = value;
        }

        public static CardNumber FromString(string number) => new CardNumber(int.Parse(number));

        public static implicit operator string(CardNumber cardNumber) => cardNumber.Value.ToString();
    }

    public enum CardName
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
}
