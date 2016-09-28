using Movie_Organizer.classs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Movie_Organizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.Read();


            Settings.MoviePath = "GODVERDOMMME";





            Settings.Write();

            //MessageBox.Show(Settings.MoviePath);



            Task.Run(new Action(Initialize));
        }

        void Initialize()
        {
            //settings.Directorytree = new List<string>();
            //settings.Directorytree.Add("test");


           


            foreach (string dir in Directory.GetDirectories(@"D:\Movies\"))
            {

            }
        }
    }
}
