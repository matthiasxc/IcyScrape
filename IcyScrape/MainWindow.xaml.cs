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

            byte[] data;
            using(WebClient client = new WebClient())
            {
                data = client.DownloadData("http://media-Hearth.cursecdn.com/avatars/148/97/548.png");
            }
            File.WriteAllBytes(@"C:\Users\BigBox\Desktop\Hearthstone Cards\xyz.png", data);



            _mvm.AllDecks = IcyScraperService.GetStandardDecks(new DateTime(2016, 8, 31), 19000);
            //_mvm.AllDecks = IcyScraperService.GetWildDecks(new DateTime(2016, 8, 31), 19000);

            _mvm.CalculateCardSet();
            _mvm.WriteCardDataOut();
            _mvm.DownloadCardImages();

        }
    }
}
