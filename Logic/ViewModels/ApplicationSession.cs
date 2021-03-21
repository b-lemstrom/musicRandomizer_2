
using Logic.EventArgs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Logic.ViewModels
{
    public class ApplicationSession : BaseNotificationClass
    {
        public event EventHandler<MessageEventArgs> OnMessageRaised;

        private PlayList _currentPlayList;
        public PlayList CurrentPlayList 
        { 
            get { return _currentPlayList; }
            set
            {
                _currentPlayList = value;
            }
        }

        public string[] lines;
        public List<string> generatedSongs = new List<string>();
        public ApplicationSession()
        {
            CurrentPlayList = new PlayList
            {
                Name = "",
                LastSong = "",
            };

            configLoad();
        }
        
        public void openPlaylist()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Playlist Files|*.m3u8";
            openFileDialog.Title = "Select a Playlist File";

            if (openFileDialog.ShowDialog() == true) //System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                config(filename);
                lines = File.ReadAllLines(filename);
                string display = openFileDialog.SafeFileName;
                display = display.Replace("_", " ");
                display = display.Replace(".m3u8", "");
                CurrentPlayList.Name = display;
            }
        }

        public string config(string playListName)
        {
            string text = playListName;
            File.WriteAllText("Config.txt", text);
            return "";
        }

        public void configLoad()
        {
            string curFile = "Config.txt";
            if(File.Exists(curFile) && File.ReadAllText("Config.txt") != null)
            {
                string filename = File.ReadAllText("Config.txt");
                lines = File.ReadAllLines(filename);
                filename = filename.Replace("_", " ");
                filename = filename.Replace(".m3u8", "");
                CurrentPlayList.Name = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            else
            {
                return;
            } 
        }

        public void randomizeSong()
        {
            //Simple random-function, between 1 and the length of the array. Is not 0 due to file structure.
            Random rnd = new Random();
            if (lines == null)
            {
               MessageBoxResult result = MessageBox.Show("Please pick a playlist first.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    openPlaylist();
                }
               //Send to OPEN PLAYLIST IF OK
            }
            else
            {
                int y = lines.Length;
                int x = rnd.Next(1, y);
                //Here we do a check, because the structure of a playlistfile gives us the filepath on every other line.
                while (x % 2 == 0)
                {
                    x = rnd.Next(1, y);
                }
                //Cleaning up the output from "#EXTINF:177,AC/DC - Miss Adventure" -> "AC/DC - Miss Adventure"
                string second = lines[x];
                second = second.Replace("#EXTINF:", "");
                second = second.Remove(0, 4);
                CurrentPlayList.LastSong = second;
                generatedSongs.Add(second);

                RaiseMessage(second);
            }
        }

        public void SavePlayList()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file|*.txt";
            saveFileDialog.Title = "Save Playlist";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, generatedSongs);
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new MessageEventArgs(message));
        }
    }
}
