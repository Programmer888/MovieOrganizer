using Movie_Organizer.classs;
using Newtonsoft.Json;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Movie_Organizer
{
    /// <summary>
    /// Interaction logic for Organizer.xaml
    /// </summary>
    public partial class Organizer : Window
    {
        TMDbClient client;
        Movie Movie;
        public Organizer(Movie movie)
        {
            InitializeComponent();
            this.Movie = movie;
            client = new TMDbClient("fddb17fbae50abb141bdc6286c3c9e28");

            FetchConfig();
        }


        private void FetchConfig()
        {
            FileInfo configJson = new FileInfo("config.json");

            Console.WriteLine("Config file: " + configJson.FullName + ", Exists: " + configJson.Exists);

            if (configJson.Exists && configJson.LastWriteTimeUtc >= DateTime.UtcNow.AddHours(-1))
            {
                Console.WriteLine("Using stored config");
                string json = File.ReadAllText(configJson.FullName, Encoding.UTF8);

                client.SetConfig(JsonConvert.DeserializeObject<TMDbConfig>(json));
            }
            else
            {
                Console.WriteLine("Getting new config");
                client.GetConfig();

                Console.WriteLine("Storing config");
                string json = JsonConvert.SerializeObject(client.Config);
                File.WriteAllText(configJson.FullName, json, Encoding.UTF8);
            }

            //Spacer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            string query = Movie.Title;//"Thor";
            Console.WriteLine(query);
            SearchContainer<SearchMovie> results = client.SearchMovie(query);

            Console.WriteLine("Searched for movies: '" + query + "', found " + results.TotalResults + " results in " +
                              results.TotalPages + " pages");

            // Let's iterate the first few hits
            foreach (SearchMovie result in results.Results.Take(5))
            {
                wrpSuggestions.Children.Add(AddMovie(result));
            }
        }

        private Border AddMovie(SearchMovie movie)
        {
            Border border = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1) };
            border.MouseDown += Border_MouseDown;

            Grid grid = new Grid { Height = 200, Width = 150 };
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Image img = new Image();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w300_and_h450_bestv2" + movie.PosterPath);
            bitmap.EndInit();

            img.Source = bitmap;

            Grid.SetRow(img, 0);
            grid.Children.Add(img);


            Label lbl = new Label { Content = movie.Title.ToString() };
            Grid.SetRow(lbl, 1);
            grid.Children.Add(lbl);

            border.Tag = movie;

            border.Child = grid;

            return border;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            SearchMovie movie = (SearchMovie)border.Tag;
            MessageBox.Show(movie.Title);
            WebClient webclient = new WebClient();
            webclient.DownloadFile("https://image.tmdb.org/t/p/w300_and_h450_bestv2" + movie.PosterPath, Movie.DirectoryPath + "\\Cover.jpg");
            this.Close();
        }
    }
}
