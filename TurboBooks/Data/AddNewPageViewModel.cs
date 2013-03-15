using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace TurboBooks.Data
{
    public class AddNewPageViewModel : INotifyPropertyChanged
    {
        public AddNewPageViewModel()
        {
            Playlists = new ObservableCollection<PlaylistViewModel>();
        }

        public ObservableCollection<PlaylistViewModel> Playlists { get; set; }

        public bool IsDataLoaded { get; set; }
        public void LoadData()
        {
            var m = new MediaLibrary();
            foreach (var p in m.Playlists)
            {
                this.Playlists.Add(new PlaylistViewModel { PlaylistName = p.Name, Duration = p.Duration, Playlist = p });
            }

            IsDataLoaded = true;
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
