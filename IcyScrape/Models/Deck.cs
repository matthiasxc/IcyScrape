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
        public string LastModifiedString { get; set; }
        public DateTime LastModified { get; set; }
        public List<Expansion> Expansions { get; set; }
        public List<Card> ClassCards { get; set; }
        public List<Card> NeutralCards { get; set;  }
        public int DustCost { get; set; }

        public void SetModified(string modified)
        {
            LastModifiedString = modified;
            // 2016/12/10
            string[] splitDate = modified.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            LastModified = new DateTime(Int32.Parse(splitDate[0]), Int32.Parse(splitDate[1]), Int32.Parse(splitDate[2]));             
        }
    }

    public class Expansion
    {
        public Expansion(string eCode)
        {
            Code = eCode;
            switch (eCode)
            {
                case "GvG":
                    Name = "Goblins v Gnomes";
                    IsStandard = false;
                    break;
                case "TGT":
                    Name = "The Grand Tournament";
                    IsStandard = true;
                    break;
                case "MSG":
                    Name = "Mean Streets of Gadgetzan";
                    IsStandard = true;
                    break;
                case "TOG":
                    Name = "Whispers of the Old Gods";
                    IsStandard = true;
                    break;
                case "LoE":
                    Name = "The League of Exporers";
                    IsStandard = true;
                    break;
                case "BrM":
                    Name = "Blackrock Mountain";
                    IsStandard = true;
                    break;
                case "Kara":
                    Name = "One Night in Karaxhan";
                    IsStandard = true;
                    break;
                case "Naax":
                    Name = "Curse of Naxxramas";
                    IsStandard = false;
                    break;
                default:
                    Name = "Classic";
                    break;

            }
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsStandard { get; private set; }

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



}
