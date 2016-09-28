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


            Settings.MoviePath = @"D:\Media\Movies\";
            Settings.SeriePath = @"D:\Media\Series\";

            Settings.Exceptions.Add("dutchreleaseteam");
            Settings.Exceptions.Add("xvid");
            Settings.Exceptions.Add("release");
            Settings.Exceptions.Add("1080");
            Settings.Exceptions.Add("1080p");
            Settings.Exceptions.Add("BRRip");
            Settings.Exceptions.Add("BDRip");
            Settings.Exceptions.Add("x264");
            Settings.Exceptions.Add("yify");
            Settings.Exceptions.Add("subs");
            Settings.Exceptions.Add("subs");
            Settings.Exceptions.Add("subs");
            Settings.Exceptions.Add("subs");
            Settings.Exceptions.Add("subs");

            Settings.Extentions.Add("avi");
            Settings.Extentions.Add("mp4");
            Settings.Extentions.Add("mkv");

            Settings.Write();

            //MessageBox.Show(Settings.MoviePath);



            Task.Run(new Action(Initialize));
        }

        void Initialize()
        {
            //settings.Directorytree = new List<string>();
            //settings.Directorytree.Add("test");

            foreach (string dir in Directory.GetDirectories(Settings.MoviePath))
            {
                string[] files = Directory.GetFiles(dir);
                bool organized = false;
                foreach(string file in files)
                {
                    if (System.IO.Path.GetFileName(file) == "Kodi") organized = true;
                }


                if (!organized)
                {
                    string name = System.IO.Path.GetFileName(dir).ToLower();
                    Console.WriteLine(name);

                    string searchquery = "";
                    foreach (char c in name)
                    {
                        char charachter = c;
                        if (char.IsLetter(c) || c == ' ' || c == '.')
                        {
                            if (c == '.') charachter = ' ';
                            searchquery += charachter;
                        }
                        else
                        {
                            break;
                        }
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        Movie movie = new Movie();
                        movie.Title = searchquery;
                        movie.DirectoryPath = dir;
                        Organizer organizer = new Organizer(movie);
                        organizer.ShowDialog();
                    });
                }
            }
        }
    }
}
