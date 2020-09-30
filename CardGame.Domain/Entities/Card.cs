using CardGame.Domain.Values;
using System;

namespace CardGame.Domain
{
    public class Card
    {
        public Id Id { get; }
        public CardNumber Number { get; }
        public CardSuite Suite { get; }
        public bool Empty { get; set; }
        public Card() { }
        public static Card Null => new Card { Empty = true};
        public Card(Id id, CardNumber number, CardSuite suite)
        {
            Id = id;
            Number = number;
            Suite = suite;
        }

        public string DisplayCardValue()
        {
            return $"{Number.Value.ToString()} of {Suite.Color.ToString()} {Suite.Shape.ToString()}";
        }
    }
}
