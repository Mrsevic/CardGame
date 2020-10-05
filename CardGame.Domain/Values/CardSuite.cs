using CardGame.Domain.Resources;
using CardGame.Framework;
using System;
using System.Collections.Generic;

namespace CardGame.Domain.Values
{
    public class CardSuite : Value<CardSuite>
    {
        internal Suite Shape { get; }
        internal ConsoleColor Color { get; }

        public CardSuite(Suite suite, ConsoleColor color)
        {
            if (color != ConsoleColor.Black && color != ConsoleColor.Red)
                throw new ArgumentException(Strings.InvalidCardColor, nameof(color));
            Shape = suite;
            Color = color;
        }
        internal static List<CardSuite> Suits => new List<CardSuite> { Club(), Spades(), Hearts(), Diamonds() };

        static CardSuite Club() =>
           new CardSuite(Suite.Club, ConsoleColor.Black);

        static CardSuite Spades() =>
            new CardSuite(Suite.Spades, ConsoleColor.Black);

        static CardSuite Hearts() =>
            new CardSuite(Suite.Hearts, ConsoleColor.Red);

        static CardSuite Diamonds() =>
            new CardSuite(Suite.Diamonds, ConsoleColor.Red);
    }

    public enum Suite
    {
        Club = 1,
        Spades,
        Hearts,
        Diamonds,
    }
}
