using IcyScrape.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyScrape.Helpers
{
    public class Sources
    {

        public static Dictionary<string, Class> GetStandardSources()
        {
            Dictionary<string, Class> sources = new Dictionary<string, Class>();
            sources.Add("http://www.icy-veins.com/hearthstone/druid-standard-decks", Class.Druid); // 19
            sources.Add("http://www.icy-veins.com/hearthstone/hunter-standard-decks", Class.Hunter); // 13
            sources.Add("http://www.icy-veins.com/hearthstone/mage-standard-decks", Class.Mage); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/paladin-standard-decks", Class.Paladin); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/priest-standard-decks", Class.Priest); // 9
            sources.Add("http://www.icy-veins.com/hearthstone/rogue-standard-decks", Class.Rogue); // 6
            sources.Add("http://www.icy-veins.com/hearthstone/shaman-standard-decks", Class.Shaman); // 19
            sources.Add("http://www.icy-veins.com/hearthstone/warlock-standard-decks", Class.Warlock); // 6
            sources.Add("http://www.icy-veins.com/hearthstone/warrior-standard-decks", Class.Warrior); //12

            return sources;

        }

        public static Dictionary<string, Class> GetWildSources()
        {
            Dictionary<string, Class> sources = new Dictionary<string, Class>();
            sources.Add("http://www.icy-veins.com/hearthstone/druid-wild-decks", Class.Druid); //4
            sources.Add("http://www.icy-veins.com/hearthstone/hunter-wild-decks", Class.Hunter); //5
            sources.Add("http://www.icy-veins.com/hearthstone/mage-wild-decks", Class.Mage); //11
            sources.Add("http://www.icy-veins.com/hearthstone/paladin-wild-decks", Class.Paladin); //8
            sources.Add("http://www.icy-veins.com/hearthstone/priest-wild-decks", Class.Priest); //9
            sources.Add("http://www.icy-veins.com/hearthstone/rogue-wild-decks", Class.Rogue); //6
            sources.Add("http://www.icy-veins.com/hearthstone/shaman-wild-decks", Class.Shaman); //6
            sources.Add("http://www.icy-veins.com/hearthstone/warlock-wild-decks", Class.Warlock); //5
            sources.Add("http://www.icy-veins.com/hearthstone/warrior-wild-decks", Class.Warrior); //7

            return sources;

        }


    }
}
