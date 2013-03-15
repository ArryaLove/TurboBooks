using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TurboBooks.Data
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        
        private TurboBookDataContext _DataContext;
        public MainViewModel(string connectionString)
        {
            _DataContext = new TurboBookDataContext(connectionString);
        }

        public ObservableCollection<Book> Books { get; private set; }

        public bool IsDataLoaded { get; private set; }
        
        public void LoadData()
        {
            

            var audioBooks = from Book book in _DataContext.Books select book;

            Books = new ObservableCollection<Book>(audioBooks);
            
            IsDataLoaded = true;
        }

        public void SaveData()
        {
            _DataContext.SubmitChanges();
        }

        public void AddBook(Book book)
        {
            _DataContext.Books.InsertOnSubmit(book);

            _DataContext.SubmitChanges();

            Books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            Books.Remove(book);

            _DataContext.Books.DeleteOnSubmit(book);

            _DataContext.SubmitChanges();
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
