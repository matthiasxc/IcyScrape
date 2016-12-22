using HtmlAgilityPack;
using IcyScrape.Helpers;
using IcyScrape.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IcyScrape.Services
{
    public static class IcyScraperService
    {

        private static List<Deck> GetStandardDecks(int datLimit, int countLimit, int dustLimit)
        {
            var standardSources = Sources.GetStandardSources();
            
            List<Deck> listOfDecks = new List<Deck>();

            // Go through all the classes source by source
            //foreach(string classUrl in )
            



            return listOfDecks;
        }

        private static List<Deck> GetWildDecks(int dayLimit, int countLimit, int dustLimit)
        {
            var standardSources = Sources.GetStandardSources();

            List<Deck> listOfDecks = new List<Deck>();


            return listOfDecks;
        }

        public static List<Deck> GetDeckSummaries(string classUrl)
        {
            List<Deck> returnListOfDecks = new List<Deck>();

            // 1) get the document
            var pageHtml = new HtmlAgilityPack.HtmlDocument();
            pageHtml.LoadHtml(new WebClient().DownloadString(classUrl));

            // 2) Find the deck table
            // <table class="deck_resentation">
            var root = pageHtml.DocumentNode;
            var deckNode = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_presentation"));
            var deckRows = deckNode.First().Descendants("tr");

            bool isFirst = true;
            // 3) Ignore the first tr (that's the header)
            foreach(HtmlNode node in deckRows)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                { 
                    Deck thisDeck = new Deck();

                    // Get deck name, URL, expansion info
                    var deckInfo = node.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_presentation_name")).Single();
                    thisDeck.Name = deckInfo.Descendants("a").Single().InnerText;
                    thisDeck.Url = deckInfo.Descendants("a").Single().Attributes["href"].Value;
                    var deckExpansions = node.Descendants("span").Where(n => n.GetAttributeValue("class", "").Equals("expansion_marker"));

                    List<string> expansions = new List<string>();
                    foreach (HtmlNode n in deckExpansions) 
                        thisDeck.Expansions.Add(new Expansion(n.InnerHtml));

                    // get the cost info
                    var costInfo = node.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_presentation_cost")).Single();
                    var cost = costInfo.InnerHtml.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries).First() ;
                    int dustCost = 0;
                    Int32.TryParse(cost, out dustCost);
                    thisDeck.DustCost = dustCost;

                    // get the date on the deck
                    var dateInfo = node.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_presentation_last_update")).Single().InnerHtml;
                    thisDeck.SetModified(dateInfo); ;

                    returnListOfDecks.Add(thisDeck);
                }
            }

            return returnListOfDecks;

            // 4) Get the decks
            //< tr >
            //  <td class="deck_presentation_name">
            //    <a href = "http://www.icy-veins.com/hearthstone/kun-malygos-druid-gadgetzan-standard-deck" >
            //      Kun Malygos Gadgetzan
            //    </a>
            //    <span class="expansion_markers"> 
            //        <span class="expansion_marker">MSG</span>  
            //        <span class="expansion_marker">TOG</span>
            //        <span class="expansion_marker">LoE</span>
            //        <span class="expansion_marker">TGT</span>
            //        <span class="expansion_marker">BrM</span>
            //    </span>
            //  </td>
            //  <td class="deck_presentation_archetype"><em>None</em></td>
            //  <td class="deck_presentation_cost">8,580 
            //      <img src = "http://static.icy-veins.com/images/hearthstone/arcane-dust.png" alt="Arcane Dust" title="Arcane Dust">
            //  </td>
            //  <td class="deck_presentation_last_update">2016/12/10</td>
            //</tr>
        }

        public static Deck GetDeck(Deck deckShell)
        {
            
            return deckShell;
        }
    }
}
