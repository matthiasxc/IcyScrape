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

        public override string ToString()
        {
            return Class.ToString() + " - " + Name;
        }
        public string Url { get; set; } 
        public string Name { get; set; }
        public string LastModifiedString { get; set; }
        public DateTime LastModified { get; set; }
        public List<Expansion> Expansions { get; set; }
        public List<Card> ClassCards { get; set; }
        public List<Card> NeutralCards { get; set;  }
        public int DustCost { get; set; }
        public Class Class { get; set; }

        public void SetModified(string modified)
        {
            LastModifiedString = modified;
            // 2016/12/10
            string[] splitDate = modified.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int monthOutput = 1;
            switch (splitDate[0].ToLower())
            {
                case "jan":
                    monthOutput = 1;
                    break;
                case "feb":
                    monthOutput = 2;
                    break;
                case "mar":
                    monthOutput = 3;
                    break;
                case "apr" :
                    monthOutput = 4;
                    break;
                case "may":
                    monthOutput = 5;
                    break;
                case "jun":
                    monthOutput = 6;
                    break;
                case "jul":
                    monthOutput = 7;
                    break;
                case "aug":
                    monthOutput = 8;
                    break;
                case "sep":
                    monthOutput = 9;
                    break;
                case "oct":
                    monthOutput = 10;
                    break;
                case "nov":
                    monthOutput = 11;
                    break;
                case "dec":
                    monthOutput = 12;
                    break;
                default:
                    monthOutput = 1;
                    break;
            }
            LastModified = new DateTime(Int32.Parse(splitDate[2]), monthOutput, Int32.Parse(splitDate[1]));             
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
                    IsStandard = false;
                    break;
                case "MSG":
                    Name = "Mean Streets of Gadgetzan";
                    IsStandard = false;
                    break;
                case "TOG":
                    Name = "Whispers of the Old Gods";
                    IsStandard = false;
                    break;
                case "LoE":
                    Name = "The League of Explorers";
                    IsStandard = false;
                    break;
                case "BrM":
                    Name = "Blackrock Mountain";
                    IsStandard = false;
                    break;
                case "Kara":
                    Name = "One Night in Karazhan";
                    IsStandard = false;
                    break;
                case "Naxx":
                    Name = "Curse of Naxxramas";
                    IsStandard = false;
                    break;
                case "K&amp;C":
                    Name = "Kobolds and Catacombs";
                    IsStandard = true;
                    break;
                case "KFT":
                    Name = "Knights of the Frozen Throne";
                    IsStandard = true;
                    break;
                case "WW":
                    Name = "The Witchood";
                    IsStandard = true;
                    break;
                case "Un'Goro":
                    Name = "Journey to Un'Goro";
                    IsStandard = true;
                    break;
                default:
                    Name = "Classic";
                    IsStandard = true;
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
            CardClass = Class.Neutral;
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Expansion Expansion { get; set; }
        public Class CardClass {get; set;}
        public int CardCost { get; set; }

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
