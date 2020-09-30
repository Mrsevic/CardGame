using System.Collections.Generic;

namespace CardGame.Domain
{
    public class Pile
    {
        public List<Card> Cards { get; set; }

        public Pile() => Cards = new List<Card>();

        public Pile(List<Card> cards) => Cards = cards;
    }
}
