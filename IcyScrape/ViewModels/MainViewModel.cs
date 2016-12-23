using IcyScrape.Models;
using IcyScrape.Services;
using IcyScrape.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IcyScrape.ViewModels
{
    public class MainViewModel :INotifyPropertyChanged
    {
        public MainViewModel()
        {
        }

        #region AllDecks (List<Deck>)
        private List<Deck> _allDecks = new List<Deck>();
        public List<Deck> AllDecks
        {
            get { return _allDecks; }
            set
            {
                _allDecks = value;
                NotifyPropertyChanged("AllDecks");
            }
        }
        #endregion

        #region AllCards (List<CardReport>)
        private List<CardReport> _allCards = new List<CardReport>();
        public List<CardReport> AllCards
        {
            get { return _allCards; }
            set
            {
                _allCards = value;
                NotifyPropertyChanged("AllCards");
            }
        }
        #endregion

        #region OutputFolder (string)
        private string _outputFolder = "";
        public string OutputFolder
        {
            get { return _outputFolder; }
            set
            {
                _outputFolder = value;
                NotifyPropertyChanged("OutputFolder");
            }
        }
        #endregion


        public void CalculateCardSet()
        {
            Dictionary<Card, int> cardCounter = new Dictionary<Card, int>();

            foreach (Deck d in AllDecks)
            {
                foreach (Card c in d.ClassCards)
                {
                    var foundCard = AllCards.Where(card => card.HearthstoneCard.Name == c.Name).FirstOrDefault();
                    if (foundCard == null) {
                        foundCard = new CardReport() { HearthstoneCard = c};
                        AllCards.Add(foundCard);                    
                    }

                    foreach (var cardReport in AllCards.Where(w => w.HearthstoneCard.Name == c.Name))
                    {
                        cardReport.CardCount++;
                        cardReport.DecksWithCard.Add(d);
                    }
                }

                foreach (Card c in d.NeutralCards)
                {
                    var foundCard = AllCards.Where(card => card.HearthstoneCard.Name == c.Name).FirstOrDefault();
                    if (foundCard == null)
                    {
                        foundCard = new CardReport() { HearthstoneCard = c };
                        AllCards.Add(foundCard);
                    }

                    foreach (var cardReport in AllCards.Where(w => w.HearthstoneCard.Name == c.Name))
                    {
                        cardReport.CardCount++;
                        cardReport.DecksWithCard.Add(d);
                    }
                }
            }
            
        }

        public void DownloadCardImages()
        {
            string dataDirectory = "";
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    dataDirectory = dlg.SelectedPath;
                }
            }

            foreach (CardReport cr in AllCards)
                cr.HearthstoneCard.ImageUrl = IcyScraperService.GetCardImageUrl(cr.HearthstoneCard.ImageUrl);


            using (WebClient wc = new WebClient())
            {
                foreach (CardReport cr in AllCards)
                {
                    byte[] data;
                    data = wc.DownloadData(cr.HearthstoneCard.ImageUrl);                     
                    File.WriteAllBytes(@"C:\Users\BigBox\Desktop\Hearthstone Cards\" + GenerateSlug(cr.HearthstoneCard.Name.ToLower()) + ".png", data);
                    System.Threading.Thread.Sleep(500);
                }
                    
            }


        }
        private static string GenerateSlug(string phrase)
        {
            string str = phrase;
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            return str;

            //// convert multiple spaces into one space   
            //str = Regex.Replace(str, @"\s+", " ").Trim();
            //// cut and trim 
            //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            //str = Regex.Replace(str, @"\s", "-"); // hyphens   
            //return str;
        }

        public void WriteCardDataOut()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            string selectedFile = "";
            if (result == true)
            {
                // Open document 
                selectedFile = dlg.FileName;

                using (CsvFileWriter writer = new CsvFileWriter(selectedFile))
                {
                    CsvRow header = new CsvRow();
                    header.Add("Card Name");
                    header.Add("Card Cost");
                    header.Add("Class");
                    header.Add("Expansion");
                    header.Add("Count");
                    header.Add("Decks");
                    writer.WriteRow(header);
                    foreach (CardReport cr in AllCards)
                    {
                        CsvRow row = new CsvRow();
                        row.Add(cr.HearthstoneCard.Name);
                        row.Add(cr.HearthstoneCard.CardCost.ToString());
                        row.Add(cr.HearthstoneCard.CardClass.ToString());
                        row.Add(cr.HearthstoneCard.Expansion.Name);
                        row.Add(cr.CardCount.ToString());
                        foreach (Deck d in cr.DecksWithCard)
                        {
                            row.Add(d.Class + " - " + d.Name);
                        }
                        writer.WriteRow(row);
                    }
                }
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
