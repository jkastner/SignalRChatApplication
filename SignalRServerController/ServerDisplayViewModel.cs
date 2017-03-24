using System.ComponentModel;
using System.Runtime.CompilerServices;
using SignalRServerController.Annotations;

namespace SignalRServerController
{
    public class ServerDisplayViewModel : INotifyPropertyChanged
    {
        private string _receievedData;
        private string _chatText;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public string ReceievedData
        {
            get { return _receievedData; }
            set
            {
                if (value == _receievedData) return;
                _receievedData = value;
                OnPropertyChanged();
            }
        }

        public string ChatText
        {
            get { return _chatText; }
            set
            {
                if (value == _chatText) return;
                _chatText = value;
                OnPropertyChanged();
            }
        }
    }
}