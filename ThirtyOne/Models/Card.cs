using System;
namespace ThirtyOne.Models
{
    public class Card
    {
        public Suits Suit { get; set; }

        public int Rank { get; set; }

        public int Value
        {
            get
            {
                return (Rank == 1) ? 11 : (Rank >= 10 && Rank < 14) ? 10 : Rank;
            }
        }

        public override string ToString()
        {
            return Rank.ToString() + " of " + Suit.ToString();
        }
    }
}
