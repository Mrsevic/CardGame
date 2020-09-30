using CardGame.Domain.Values;
using CardGame.Framework;
using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Domain
{
    public class Player
    {
        public Id Id { get; set; }
        public Pile DrawPile { get; set; }
        public Pile DiscardPile { get; set; }
        public string Nick { get; set; }

        public static string ValidateName(string player)
        {
            while (string.IsNullOrEmpty(player) || string.IsNullOrWhiteSpace(player))
            {
                player = Input.ReadString("Name cannot be blank, please enter your name:");
            }
            return player;
        }

        public Player()
        {
            DrawPile = new Pile();
            DiscardPile = new Pile();
        }

        public Player(Id id) : this()
        {
            Id = id;
        }

        public Card DrawCard()
        {
            Card pulledCard;
            if (DrawPile.Cards.Count > 0)
            {
                pulledCard = DrawPile.Cards[0];

                DrawPile.Cards.Remove(pulledCard);
                Output.WriteLine($"Number of cards in the drawing pile of {Nick} : {DrawPile.Cards.Count.ToString()} {Environment.NewLine}");
            }
            else
            {
                Output.WriteLine($"No more cards in the drawing pile of {Nick}, taking the cards from the discard pile and inserting them back to the drawing pile... {Environment.NewLine}");
                DiscardPile.Cards = DiscardPile.Cards.Shuffle().ToList();
                DrawPile.Cards.AddRange(DiscardPile.Cards);
                DiscardPile.Cards.Clear();
                pulledCard = DrawPile.Cards.Count > 0 ? DrawPile.Cards[0] : Card.Null;
                if (pulledCard.Empty == false)
                {
                    DrawPile.Cards.Remove(pulledCard);
                }
            }
            return pulledCard;
        }

        public void PopulateDiscardPile(IEnumerable<Card> cards)
        {
            this.DiscardPile.Cards.AddRange(cards);
        }
        public static List<Player> BuildPlayers(List<Pile> piles, int noPlayers = 2)
        {
            void EquipPlayer(Player player, Pile pile, int index)
            {
                var nick = Input.ReadString($"Insert the name for the player No. {index.ToString()}:");
                player.Nick = ValidateName(nick);
                player.DrawPile = pile;
            }

            var players = new List<Player>(noPlayers);

            for (int i = 0; i < noPlayers; i++)
            {
                players.Add(new Player(new Id(Guid.NewGuid())));
                EquipPlayer(players[i], piles[i], i);
            }
            return players;
        }
    }
}
