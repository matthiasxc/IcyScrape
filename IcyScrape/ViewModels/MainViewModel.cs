using IcyScrape.Models;
using IcyScrape.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void WriteCardDataOut()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            string selectedFile = "";
            // Get the selected file name and display in a TextBox 
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
                        foreach(Deck d in cr.DecksWithCard)
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
