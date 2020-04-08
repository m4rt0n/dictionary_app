using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test1
{
    public class Word : INotifyPropertyChanged
    {

        private int id { get; set; }
        private string wordEng;
        //private string wordRus;
        //private string wordNote;
        private string wordPicturePath;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                // notify listeners of event (bound controls (labels, ..))
                OnPropertyChanged("Id");
            }
        }
        public string WordEng
        {
            get
            {
                return wordEng;
            }
            set
            {
                wordEng = value;
                OnPropertyChanged("WordEng");
            }
        }
        /*
        public string WordRus
        {
            get
            {
                return wordRus;
            }
            set
            {
                wordRus = value;
                OnPropertyChanged("WordRus");
            }
        }
        public string WordNote
        {
            get
            {
                return wordNote;
            }
            set
            {
                wordNote = value;
                OnPropertyChanged("WordNote");
            }
        }
        */
        public string WordPicturePath
        {
            get
            {
                return wordPicturePath;
            }
            set
            {
                wordPicturePath = value;
                OnPropertyChanged("WordPicturePath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}