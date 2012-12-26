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
        public MainViewModel()
        {
            AudioBooks = new ObservableCollection<AudioBookBookModel>();
        }

        public ObservableCollection<AudioBookBookModel> AudioBooks { get; private set; }

        public bool IsDataLoaded { get; private set; }
        public void LoadData()
        {
            if (File.Exists("Data.xml"))
            {
                using (var iStream = new FileStream("Data.xml", FileMode.Open, FileAccess.Read))
                {
                    var x = XDocument.Load(iStream);

                    foreach (var book in x.Elements("AudioBook"))
                    {
                        AudioBooks.Add(new AudioBookBookModel
                        {
                            Name = book.Attribute("name").Value,
                            NumberOfItems = int.Parse(book.Attribute("numberOfItems").Value)
                        });
                    }
                }
            }
            IsDataLoaded = true;
        }

        public void SaveData()
        {
            var x = new XDocument();
            var root = new XElement("Data");
            x.Add(root);
            var audioBooksElement = new XElement("AudioBooks");
            root.Add(audioBooksElement);
            foreach (var book in AudioBooks)
            {
                audioBooksElement.Add(new XElement("AudioBook",
                    new XAttribute("name", book.Name),
                    new XAttribute("numberOfItems", book.NumberOfItems)));
            }
            using (var oStream = new FileStream("Data.xml", FileMode.Create, FileAccess.Write))
            {
                x.Save(oStream);
            }
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
