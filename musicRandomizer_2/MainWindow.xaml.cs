using System.Windows;
using System.Windows.Documents;
using Logic.EventArgs;
using Logic.ViewModels;

namespace musicRandomizer_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationSession _appSession = new ApplicationSession();
        public MainWindow()
        {
            InitializeComponent();

            GeneratedSongs.Document.Blocks.Add(new Paragraph(new Run("Generated songs" + "\n")));

            _appSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _appSession;
        }

        private void OnClick_OpenPlaylist(object sender, RoutedEventArgs e)
        {
            _appSession.openPlaylist();
        }

        private void OnClick_rndSong(object sender, RoutedEventArgs e)
        {
            _appSession.randomizeSong();
        }

        private void OnGameMessageRaised(object sender, MessageEventArgs e)
        {
            GeneratedSongs.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GeneratedSongs.ScrollToEnd();
        }

        private void SaveGeneratedSongs_OnClick(object sender, RoutedEventArgs e)
        {
            _appSession.SavePlayList();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tool to randomly generate songs from a playlist file.");
        }
    }
}
