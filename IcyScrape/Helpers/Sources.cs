using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyScrape.Helpers
{
    public class Sources
    {

        public static List<string> GetStandardSources()
        {
            List<string> sources = new List<string>();
            sources.Add("http://www.icy-veins.com/hearthstone/druid-standard-decks"); // 19
            sources.Add("http://www.icy-veins.com/hearthstone/hunter-standard-decks"); // 13
            sources.Add("http://www.icy-veins.com/hearthstone/mage-standard-decks"); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/paladin-standard-decks"); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/priest-standard-decks"); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/rogue-standard-decks"); // 6
            sources.Add("http://www.icy-veins.com/hearthstone/shaman-standard-decks"); // 19
            sources.Add("http://www.icy-veins.com/hearthstone/warlock-standard-decks"); // 6
            sources.Add("http://www.icy-veins.com/hearthstone/warrior-standard-decks"); //12

            return sources;

        }

        public static List<string> GetWildSources()
        {
            List<string> sources = new List<string>();
            sources.Add("http://www.icy-veins.com/hearthstone/druid-wild-decks"); //4
            sources.Add("http://www.icy-veins.com/hearthstone/hunter-wild-decks"); //5
            sources.Add("http://www.icy-veins.com/hearthstone/mage-wild-decks"); //11
            sources.Add("http://www.icy-veins.com/hearthstone/paladin-wild-decks"); //8
            sources.Add("http://www.icy-veins.com/hearthstone/priest-wild-decks"); //9
            sources.Add("http://www.icy-veins.com/hearthstone/rogue-wild-decks"); //6
            sources.Add("http://www.icy-veins.com/hearthstone/shaman-wild-decks"); //6
            sources.Add("http://www.icy-veins.com/hearthstone/warlock-wild-decks"); //5
            sources.Add("http://www.icy-veins.com/hearthstone/warrior-wild-decks"); //7

            return sources;

        }


    }
}
