using CardGame.Domain.Resources;
using CardGame.Domain.Values;
using CardGame.Framework;
using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Domain
{
    public class Deck
    {
        public List<Card> TotalCards { get; set; }

        public Deck() => TotalCards = new List<Card>();

        public Deck Initialize(int desiredCapacity, int noPlayers = 2)
        {
            (desiredCapacity, _) = EnsureValidity(desiredCapacity, noPlayers);

            const int maxCardNumber = 10;
            var noOfAppereances = desiredCapacity / maxCardNumber;

            for (int i = 1; i <= maxCardNumber; i++)
            {
                for (int j = 1; j <= noOfAppereances; j++)
                {
                    if (j <= CardSuite.Suits.Count)
                        TotalCards.Add(new Card(new Id(Guid.NewGuid()), new CardNumber(i), CardSuite.Suits[j - 1]));
                    else
                        TotalCards.Add(new Card(new Id(Guid.NewGuid()), new CardNumber(i), CardSuite.Suits[j - CardSuite.Suits.Count]));
                }
            }
            Output.WriteLine("Number of cards in the deck: " + TotalCards.Count.ToString() + "\n\n");
            return this;
        }

        public void Mix()
        {
            TotalCards = TotalCards.Shuffle().ToList();
        }

        public List<Pile> DivideTheCardsForThePlayers(int noPlayers = 2)
        {
            var cardsNoPerPlayer = TotalCards.Count / noPlayers;

            var cardsGroupped = TotalCards.Split(cardsNoPerPlayer)
                                .Select(x => new Pile(x.OrderBy(r => r.Suite.Shape).ToList())).ToList();
            return cardsGroupped;
        }

        private (int, int) EnsureValidity(int capacity, int noPlayers)
        {
            while (capacity < 10)
            {
                Output.WriteLine(Strings.InvalidDeckCapacityLessThenCardNumber);
                capacity = Input.ReadInt();
            }

            while (capacity == 0 || capacity % 2 > 0 || capacity % 10 > 0)
            {
                Output.WriteLine(Strings.InvalidDeckCapacity);
                capacity = Input.ReadInt();
            }

            while (noPlayers < 2 && noPlayers % 2 > 0 && capacity % noPlayers > 0)
            {
                Output.WriteLine(Strings.InvalidPlayerNumber);
                noPlayers = Input.ReadInt();
            }

            return (capacity, noPlayers);
        }
    }
}
