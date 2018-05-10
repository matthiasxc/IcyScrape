using IcyScrape.Services;
using IcyScrape.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IcyScrape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            MainViewModel _mvm = new MainViewModel();

            this.DataContext = _mvm;

            ////_mvm.AllDecks = IcyScraperService.GetStandardDecks(new DateTime(2016, 8, 31), 19000);
            //_mvm.AllDecks = IcyScraperService.GetWildDecks(new DateTime(2016, 8, 31), 19000);

            //_mvm.CalculateCardSet();
            //_mvm.WriteCardDataOut();
            //_mvm.DownloadCardImages();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel _mvm = (MainViewModel)this.DataContext;
            _mvm.Status = "Loading Decks...";
            _mvm.AllDecks = IcyScraperService.GetStandardDecks(new DateTime(2018, 2, 01), 32000);
            //_mvm.AllDecks = IcyScraperService.GetWildDecks(new DateTime(2016, 8, 31), 19000);
            _mvm.Status = "Calculating Card Sets";
            _mvm.CalculateCardSet();
            _mvm.Status = "Exporting Data";
            _mvm.WriteCardDataOut();
            _mvm.Status = "Downloading images";
            _mvm.DownloadCardImages();
            _mvm.Status = "Done";
        }
    }
}
