using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServerEngine
{
    public class BaseNotificationClass
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
