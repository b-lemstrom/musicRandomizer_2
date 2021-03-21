using System.ComponentModel;

namespace Logic
{
    public class BaseNotificationClass : INotifyPropertyChanged
    {
        //Event to send a message to the listener if anything is changed.
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}