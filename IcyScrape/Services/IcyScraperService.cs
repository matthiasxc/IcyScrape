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



        public static List<Deck> GetStandardDecks(DateTime dateLimit, int dustLimit)
        {
            var standardSources = Sources.GetStandardSources();
            
            List<Deck> listOfDecks = new List<Deck>();

            // Go through all the classes source by source & get the deck list
            foreach(KeyValuePair<string, Class> kvp in standardSources)
            {
                var deckList = GetDeckSummaries(kvp.Key, kvp.Value);

                foreach (Deck d in deckList)
                    listOfDecks.Add(d);
            }

            // Go through the deck list and get the cards         
            for(int i =0; i <  listOfDecks.Count; i++)
            {
                listOfDecks[i] = GetDeck(listOfDecks[i]);
            }

            //return listOfDecks;
            var limitedDecks = listOfDecks.Where(d => d.LastModified > dateLimit).ToList();
            
            return limitedDecks;
        }

        public static List<Deck> GetWildDecks(DateTime dateLimit, int dustLimit)
        {
            var wildSources = Sources.GetWildSources();

            List<Deck> listOfDecks = new List<Deck>();

            // Go through all the classes source by source & get the deck list
            foreach (KeyValuePair<string, Class> kvp in wildSources)
            {
                var deckList = GetDeckSummaries(kvp.Key, kvp.Value);

                foreach (Deck d in deckList)
                    listOfDecks.Add(d);
            }

            // Go through the deck list and get the cards         
            for (int i = 0; i < listOfDecks.Count; i++)
            {
                listOfDecks[i] = GetDeck(listOfDecks[i]);
            }

            var limitedDecks = listOfDecks.Where(d => d.LastModified > dateLimit).ToList();

            return listOfDecks;
        }

        public static List<Deck> GetDeckSummaries(string classUrl, Class deckClass)
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
                    var cost = costInfo.InnerHtml.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries).First() ;
                    int dustCost = 0;
                    Int32.TryParse(cost.Replace(",", ""), out dustCost);
                    thisDeck.DustCost = dustCost;

                    // get the date on the deck
                    var dateInfo = node.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_presentation_last_update")).Single().InnerHtml;
                    thisDeck.SetModified(dateInfo); ;
                    thisDeck.Class = deckClass;
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
            List<Card> classCards = new List<Card>();
            List<Card> neutralCards = new List<Card>();

            // 1) get the document
            var pageHtml = new HtmlAgilityPack.HtmlDocument();
            pageHtml.LoadHtml(new WebClient().DownloadString(deckShell.Url));

            // 2) Find the card table
            // <table class="deck_resentation">
            var root = pageHtml.DocumentNode;
            var deckNode = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("deck_card_list"));
            var deckLists = deckNode.First().Descendants("ul");

            // The first one will be the class cards, the second one will be neutral
            int listNum = 0;
            foreach (HtmlNode cardList in deckLists)
            {
                var cards = cardList.Descendants("li");
                foreach(HtmlNode card in cards)
                {
                    Card thisCard = new Card();
                    //if (card.ChildNodes[0].InnerText.Contains("2x"))
                    //    thisCard.Count = 2;
                    //else if (card.ChildNodes[0].InnerText.Contains("1x"))
                    //    thisCard.Count = 1;

                    if (card.InnerHtml.Contains(" q1"))
                        thisCard.CardCost = 40;
                    else if (card.InnerHtml.Contains(" q3"))
                        thisCard.CardCost = 100;
                    else if (card.InnerHtml.Contains(" q4"))
                        thisCard.CardCost = 400;
                    else if (card.InnerHtml.Contains(" q5"))
                        thisCard.CardCost = 1600;

                    thisCard.Name = card.ChildNodes[1].InnerText;

                    if (card.ChildNodes.Count > 3)
                        thisCard.Expansion = new Expansion(card.ChildNodes[3].InnerText);
                    else
                        thisCard.Expansion = new Expansion("none");

                    if (listNum == 0)
                    {
                        thisCard.CardClass = deckShell.Class;
                        classCards.Add(thisCard);
                    }
                    else
                    {
                        neutralCards.Add(thisCard);
                    }

                }
                listNum++;
            }

            deckShell.ClassCards = classCards;
            deckShell.NeutralCards = neutralCards;

            return deckShell;
        }
    }
}
