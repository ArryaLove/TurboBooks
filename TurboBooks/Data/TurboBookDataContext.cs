using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboBooks.Data
{
    public enum BookType
    {
        SystemPlaylist,
        AppPlaylist
    }

    public class TurboBookDataContext : DataContext
    {
        public TurboBookDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<Book> Books;

        public Table<Bookmark> Bookmarks;
    }

    [Table]
    public class Book : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BookId { get; set; }

        [Column]
        public string BookName { get; set; }

        [Column]
        public BookType BookType { get; set; }

        private EntitySet<Bookmark> _bookmarks;

        [Association(Storage = "_bookmarks", OtherKey = "_bookId", ThisKey = "BookId")]
        public EntitySet<Bookmark> Bookmarks
        {
            get { return this._bookmarks; }
            set { this._bookmarks.Assign(value); }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        public Book()
        {
            _bookmarks = new EntitySet<Bookmark>(AttachBookmark, DetachBookmark);
        }

        private void DetachBookmark(Bookmark obj)
        {
            NotifyPropertyChanging("Bookmarks");
            obj.Book = null;
        }

        private void AttachBookmark(Bookmark obj)
        {
            NotifyPropertyChanging("Bookmarks");
            obj.Book = this;
        }

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Used to notify that a property changed
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;
    }

    [Table]
    public class Bookmark
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BookmarkId { get; set; }

        [Column]
        public int SongIndex { get; set; }

        [Column]
        public int Position { get; set; }

        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _bookId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Book> _book;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_book", ThisKey = "_bookId", OtherKey = "BookId", IsForeignKey = true)]
        public Book Book
        {
            get { return _book.Entity; }
            set
            {
                _book.Entity = value;
                if (value != null)
                {
                    _bookId = value.BookId;
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }
}
