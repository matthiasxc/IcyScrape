using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyScrape.Models
{
    public class Deck
    {
        public Deck()
        {
            Expansions = new List<Expansion>();
        }

        public string Url { get; set; } 
        public string Name { get; set; }
        public List<Expansion> Expansions { get; set; }
        public List<Card> ClassCards { get; set; }
        public List<Card> NeutralCards { get; set;  }
    }

    public class Expansion
    {
        public Expansion() { }

        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsStandard { get; set; }
    }

    public class Card
    {
        public Card()
        {
            Count = 1;
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
        public Expansion Expansion { get; set; }
        public Class CardClass {get; set;}

        //<li>
        //  2x
        //  <a class="hearthstone_tooltip_link q1" 
        //      data-tooltip-href="http://www.hearthpwn.com/cards/22329" 
        //      href="http://www.icy-veins.com/hearthstone/cards/druid/living-roots">
        //  Living Roots</a>  
        //  <span class="expansion_marker">
        //      TGT
        //  </span></li>

    }

    public enum Class
    {
        Druid, 
        Hunter, 
        Mage, 
        Paladin, 
        Priest, 
        Rogue, 
        Shaman, 
        Warlock, 
        Warrior, 
        Neutral,
        GrimyGoons, // Hunter, Warrior, Paladin
        JadeLotus, // Druid, Rogue, Shaman
        Kabal // Priest, Mage, Warlock
        
    }

    public enum Expand
    {

    }
}
