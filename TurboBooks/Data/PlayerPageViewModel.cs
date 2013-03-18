using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboBooks.Data
{
    public class PlayerPageViewModel
    {
        public Book Book { get; set; }

        public PlayerPageViewModel() { }

        public PlayerPageViewModel(Book book)
        {
            Book = book;
            Bookmarks = new ObservableCollection<Bookmark>(Book.Bookmarks);
            switch (Book.BookType)
            {
                case BookType.SystemPlaylist:
                    var m = new MediaLibrary();
                    var playlist = m.Playlists.SingleOrDefault(p => p.Name == Book.BookName);
                    if (playlist == null)
                    {
                        IsValid = false;
                        return;
                    }
                    Songs = new ObservableCollection<SongItem>(from s in playlist.Songs orderby s.TrackNumber select new SongItem { Song = s, SongName = s.Name });
                    break;
                default:
                    break;
            }
            IsValid = true;
        }

        public Boolean IsValid { get; set; }

        public ObservableCollection<Bookmark> Bookmarks { get; set; }
        public ObservableCollection<SongItem> Songs { get; set; }
    }

    public class SongItem
    {
        public string SongName { get; set; }
        public Song Song { get; set; }
    }
}
