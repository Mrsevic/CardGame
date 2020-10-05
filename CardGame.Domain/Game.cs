using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Domain
{
    public class Game : IGame
    {
        public List<IPlayer> Players { get; set; }
        public HashSet<Card> TemporarlyStash { get; set; }
        public Game()
        {
            Players = new List<IPlayer>();
            TemporarlyStash = new HashSet<Card>();
        }
        bool GameDone => Players.Any(v => (v.DrawPile.Cards.Count + v.DiscardPile.Cards.Count) == 0);
        public void Prepare(int deckCapacity)
        {
            var deck = new Deck().Initialize(desiredCapacity: deckCapacity) as IDeck;
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
            var scores = new Dictionary<IPlayer, Card>(Players.Count);
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
            if (winner.Empty == Player.None.Empty)
            {
                Output.WriteLine(ConsoleColor.Yellow, "Tie");
                TemporarlyStash.UnionWith(cardsToGive);
            }
            else
            {
                winner.PopulateDiscardPile(cardsToGive);
            }
            Output.WriteLine("****************************************");
        }
        public static IPlayer Confront(Dictionary<IPlayer, Card> scores)
        {
            /*foreach (var item in scores.Keys)
            {

            }*/

            if (scores.ElementAt(0).Value.Number.Value > scores.ElementAt(1).Value.Number.Value)
            {
                Output.WriteLine(ConsoleColor.Green, $"{scores.ElementAt(0).Key.Nick} wins the round.");
                return scores.ElementAt(0).Key;
            }
            if (scores.ElementAt(0).Value.Number.Value < scores.ElementAt(1).Value.Number.Value)
            {
                Output.WriteLine(ConsoleColor.Green, $"{scores.ElementAt(1).Key.Nick} wins the round.");
                return scores.ElementAt(1).Key;
            }
            return Player.None;
        }
        public void EndGame()
        {
            Output.WriteLine($"The game has ended. {Environment.NewLine}");
            var winner = Players.Aggregate((p1, p2) => p1.DrawPile.Cards.Count > p2.DrawPile.Cards.Count ? p1 : p2);
            Output.WriteLine($"{winner.Nick} wins the game");
        }
    }

    public interface IGame
    {
        List<IPlayer> Players { get; }

        void DisplayScore();
        void EndGame();
        void Play();
        void Prepare(int deckCapacity);
        void Round();
    }
}
