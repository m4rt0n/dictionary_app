using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Test1.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private WordModel wordmodel;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get { return wordmodel.Id; }
            set
            {
                wordmodel.Id = value;
                OnPropertyChanged("Id");
            }
        }
        public string WordEng
        {
            get { return wordmodel.WordEng; }
            set
            {
                wordmodel.WordEng = value;
                OnPropertyChanged("WordEng");
            }
        }
        public string WordRus
        {
            get { return wordmodel.WordRus; }
            set
            {
                wordmodel.WordRus = value;
                OnPropertyChanged("WordRus");
            }
        }
        public string WordNote
        {
            get { return wordmodel.WordNote; }
            set
            {
                wordmodel.WordNote = value;
                OnPropertyChanged("WordNote");
            }
        }
        public string WordPicturePath
        {
            get { return wordmodel.WordPicturePath; }
            set
            {
                wordmodel.WordPicturePath = value;
                OnPropertyChanged("WordPicturePath");
            }
        }

       
    }
}
