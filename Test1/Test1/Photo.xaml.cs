using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace Test1
{

    public partial class Photo : ContentPage, INotifyPropertyChanged
    {
        private Word original;

        public Word Context
        {
            get
            {
                return this.BindingContext as Word;
            }
        }

        public ObservableCollection<Word> Words { get; set; }

        const string _picsDir = @"\storage\emulated\0\android\data\com.companyname.camtest\files\Pictures\";
        string _photoPath = Path.Combine(_picsDir + "photo.jpg");

        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                _photoPath = value;
                OnPropertyChanged();
            }
        }

        public Photo(Word context, ObservableCollection<Word> words)
        {
            InitializeComponent();
            Words = words;
            original = context;
            var temp = new Word()
            {
                Id = original.Id,
                WordPicturePath = original.WordPicturePath
            };
            this.BindingContext = temp;
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                CompressionQuality = 10,
            });


            if (photo == null)
                return;

            var jpgCount = Directory.GetFiles(Path.GetDirectoryName(photo.Path)).Where(path => path.Contains(".jpg")).Count();
            var msg = $"{nameof(photo.Path)} = {photo.Path}\n" +
                $"Jpegs in app dir = {jpgCount}\n";

            await DisplayAlert("Photo saved: ", msg, "OK");

            PhotoPath = photo.Path;
            Context.WordPicturePath = PhotoPath;
            original.WordPicturePath = Context.WordPicturePath;

            if (original.Id == 0)
            {
                await App.Repo.AddItemAsync(original);
                Words.Add(original);
            }
            else
            {
                await App.Repo.UpdateItemAsync(original);
            }
            await Navigation.PopAsync();
        }

    }
}