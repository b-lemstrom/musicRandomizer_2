

using Microsoft.Win32;
using System.IO;

namespace Logic
{
    public class PlayList : BaseNotificationClass
    {
        private string _name;
        private string _lastsong;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastSong
        {
            get { return _lastsong; }
            set
            {
                _lastsong = value;
                OnPropertyChanged(nameof(LastSong));
            }
        }
    }
}
