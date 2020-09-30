using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Domain
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public HashSet<Card> TemporarlyStash { get; set; }
        public Game()
        {
            Players = new List<Player>();
            TemporarlyStash = new HashSet<Card>();
        }
        public bool GameDone
        {
            get
            {
                return Players.Any(v => (v.DrawPile.Cards.Count + v.DiscardPile.Cards.Count) == 0);
            }
        }
        public void Prepare(int deckCapacity)
        {
            var deck = new Deck().Initialize(desiredCapacity: deckCapacity);
            deck.Mix();
            var piles = deck.DivideTheCardsForThePlayers();
            Players = Player.BuildPlayers(piles);
        }
        public void DisplayScore()
        {
            foreach (var player in Players)
            {
                Output.WriteLine($"Player {player.Nick}- Draw pile: ({player.DrawPile.Cards.Count.ToString()}) Discard pile: ({player.DiscardPile.Cards.Count.ToString()}){Environment.NewLine}");

                Output.WriteLine("******************************************");
            }
            if (TemporarlyStash.Count > 0)
            {
                Output.WriteLine(ConsoleColor.Red, $"Global temporarly stash: ({ TemporarlyStash.Count.ToString()}) {Environment.NewLine}");
            }
        }
        public void Play()
        {
            while (GameDone == false)
            {
                Round();
                DisplayScore();
                //Input.ReadString(Strings.NextRound);
            }

            EndGame();
        }
        public void Round()
        {
            var scores = new Dictionary<Player, Card>(Players.Count);
            foreach (var player in Players)
            {
                Card card = player.DrawCard();
                Output.WriteLine($"{player.Nick} drew {card.DisplayCardValue()} {Environment.NewLine}");
                scores.Add(player, card);
            }
            var cardsToGive = new List<Card>(scores.Select(v => v.Value));
            if (TemporarlyStash.Count > 0)
            {
                cardsToGive.AddRange(TemporarlyStash);
                TemporarlyStash.Clear();
            }

            var winner = Confront(scores);
            winner?.PopulateDiscardPile(cardsToGive);
            if (winner == null)
            {
                Output.WriteLine(ConsoleColor.Yellow, "Tie");
                TemporarlyStash.UnionWith(cardsToGive);
            }
            Output.WriteLine("****************************************");
        }

        public static Player Confront(Dictionary<Player, Card> scores)
        {
            if (scores.ElementAt(0).Value.Number.Value > scores.ElementAt(1).Value.Number.Value)
            {
                Output.WriteLine(ConsoleColor.Green, $"{scores.ElementAt(0).Key.Nick} wins the round.");
                return scores.ElementAt(0).Key;
            }
            else if (scores.ElementAt(0).Value.Number.Value < scores.ElementAt(1).Value.Number.Value)
            {
                Output.WriteLine(ConsoleColor.Green, $"{scores.ElementAt(1).Key.Nick} wins the round.");
                return scores.ElementAt(1).Key;
            }
            else
            {
                return null;
            }
        }

        public void EndGame()
        {
            Output.WriteLine($"The game has ended. {Environment.NewLine}");
            var winner = Players.Aggregate((p1, p2) => p1.DrawPile.Cards.Count > p2.DrawPile.Cards.Count ? p1 : p2);
            Output.WriteLine($"{winner.Nick} wins the game");
        }
    }
}
