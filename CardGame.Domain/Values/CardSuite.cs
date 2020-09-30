using CardGame.Domain.Resources;
using CardGame.Framework;
using System;
using System.Collections.Generic;

namespace CardGame.Domain.Values
{
    public class CardSuite : Value<CardSuite>
    {
        public Suite Shape { get; }
        public ConsoleColor Color { get; }

        public CardSuite(Suite suite, ConsoleColor color)
        {
            if (color != ConsoleColor.Black && color != ConsoleColor.Red)
                throw new ArgumentException(Strings.InvalidCardColor, nameof(color));
            Shape = suite;
            Color = color;
        }
        public static List<CardSuite> Suits => new List<CardSuite>() { Club(), Spades(), Hearts(), Diamonds() };

        private static CardSuite Club() =>
           new CardSuite(Suite.Club, ConsoleColor.Black);

        private static CardSuite Spades() =>
            new CardSuite(Suite.Spades, ConsoleColor.Black);

        private static CardSuite Hearts() =>
            new CardSuite(Suite.Hearts, ConsoleColor.Red);

        private static CardSuite Diamonds() =>
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
