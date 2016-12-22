using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyScrape.Models
{
    public class CardReport
    {
        public CardReport() {
            DecksWithCard = new List<Deck>();
        }

        public Card HearthstoneCard { get; set; }

        public int CardCount { get; set; }

        public List<Deck> DecksWithCard { get; set; }

    }
}
