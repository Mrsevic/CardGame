using CardGame.Domain;
using CardGame.Domain.Values;
using System;
using System.Collections.Generic;
using Xunit;

namespace CardGame.Tests
{
    public class GameSpec
    {
        const int capacity = 40;

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        void DeckShouldContainNCardsDividibleByTen(int maxCapacity)
        {
            var deck = new Deck().Initialize(maxCapacity);
            Assert.Equal(maxCapacity, deck.TotalCards.Count);
        }

        // Tests are ok, however the shuffle test is comparing list references instead of elements
        [Fact]
        void DeckShouldBeShuffled()
        {
            var deck = new Deck().Initialize(capacity);
            var originalCards = new List<Card>(deck.TotalCards);
            deck.Mix();
            var shuffledCards = deck.TotalCards;
            Assert.NotEqual(originalCards, shuffledCards);
        }

        [Fact]
        void ShouldBeAbleToDrawMultipleTimesInARow()
        {
            var deck = new Deck().Initialize(capacity);
            deck.Mix();
            var piles = deck.DivideTheCardsForThePlayers();
            var player = new Player(new Id(Guid.NewGuid()), piles[0], "N.A");

            var ex = Record.Exception(() => {
                for (int i = 0; i < 30; i++)
                {
                    player.DrawCard();
                }
            });

            Assert.Null(ex);
        }

        [Fact]
        void ShouldBeAbleToDrawFromEmptyDrawingDeck()
        {
            var deck = new Deck().Initialize(capacity);
            deck.Mix();
            var piles = deck.DivideTheCardsForThePlayers();
            var player = new Player(new Id(Guid.NewGuid()), piles[0], "N.A");
            player.DiscardPile = piles[1];
            for (int i = 0; i < 30; i++)
            {
                player.DrawCard();
            }
            Assert.Equal(10, player.DrawPile.Cards.Count);
        }

        [Fact]
        void PlayerWithHigherCardNumberShouldWin()
        {
            var deck = new Deck().Initialize(capacity);
            deck.Mix();
            var piles = deck.DivideTheCardsForThePlayers();

            var player1 = new Player(new Id(Guid.NewGuid()), piles[0], "Vladimir");
            var player2 = new Player(new Id(Guid.NewGuid()), piles[1], "Stefan");

            var card1 = new Card(new Id(Guid.NewGuid()), new CardNumber(10), new CardSuite(Suite.Club, ConsoleColor.Red));
            var card2 = new Card(new Id(Guid.NewGuid()), new CardNumber(4), new CardSuite(Suite.Spades, ConsoleColor.Black));

            var scores = new Dictionary<IPlayer, Card>(2) { {player1,  card1 }, { player2, card2 } };
            var winner = Game.Confront(scores);
            Assert.Equal("Vladimir", winner.Nick);
        }

        [Fact]
        void WinnerOfTheNextRoundShould()
        {
            var deck = new Deck().Initialize(capacity);
            deck.Mix();
            var piles = deck.DivideTheCardsForThePlayers();

            var player1 = new Player(new Id(Guid.NewGuid()), piles[0], "Vladimir");
            var player2 = new Player(new Id(Guid.NewGuid()), piles[1], "Stefan");

            var card1 = new Card(new Id(Guid.NewGuid()), new CardNumber(4), new CardSuite(Suite.Club, ConsoleColor.Red));
            var card2 = new Card(new Id(Guid.NewGuid()), new CardNumber(4), new CardSuite(Suite.Club, ConsoleColor.Red));
            var scores = new Dictionary<IPlayer, Card>(2) { { player1, card1 }, { player2, card2 } };

            var game = new Game { Players = new List<IPlayer> { player1, player2} };
            var winner = Game.Confront(scores);
            var cardsToGive = new List<Card> { card1, card2 };
            winner?.PopulateDiscardPile(cardsToGive);
            if (winner == null)
            {
                game.TemporarlyStash.UnionWith(cardsToGive);
            }
            game.Round();
            var winnersPile = game.Players.Find(v => v.DiscardPile.Cards.Count > 0).DiscardPile.Cards.Count;
            Assert.Equal(4, winnersPile);
        }
    }
}