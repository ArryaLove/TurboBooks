using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.Windows.Input;
using System.Windows.Navigation;

namespace TurboBooks.Data
{
    public class PlaylistViewModel : INotifyPropertyChanged
    {
        public string PlaylistName { get; set; }
        public TimeSpan Duration { get; set; }
        public Playlist Playlist { get; set; }
        
        public PlaylistViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
