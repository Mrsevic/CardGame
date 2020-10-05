using CardGame.Domain.Values;
using System;

namespace CardGame.Domain
{
    public class Card
    {
        Id Id { get; }
        internal CardNumber Number { get; }
        internal CardSuite Suite { get; }
        internal bool Empty { get; set; }
        internal static Card None => new Card { Empty = true };

        public Card() { }

        public Card(Id id, CardNumber number, CardSuite suite)
        {
            Id = id;
            Number = number;
            Suite = suite;
        }

        internal string DisplayCardValue()
        {
            return $"{Number.Value.ToString()} of {Suite.Color.ToString()} {Suite.Shape.ToString()}";
        }
    }
}
