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
                // The binding context is the item set in the constructor.
                // It needs to be casted from 'Object' to 'Item'
                return this.BindingContext as Word;
            }
        }

        public ObservableCollection<Word> Words { get; set; }

        const string _picsDir = @"\storage\emulated\0\android\data\com.companyname.camtest\files\Pictures\";
        string _photoPath = Path.Combine(_picsDir + "SomeJpegNameAssociatedWithTheItemWhichWasPulledFromDatabase.jpg");

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
            //this.contextItem = photo;
            //BindingContext = this;

            // save reference
            Words = words;
            // save reference to original
            original = context;

            // Create temporary as a copy to bind so that when temp changes
            // through binding, the original will not be modified.
            // If we wouldn't be using a temp object, changes would be applied to
            // locally even without pressing save
            var temp = new Word()
            {
                Id = original.Id,
                WordPicturePath = original.WordPicturePath
            };

            // the temp will be the bound object
            this.BindingContext = temp;
        }

        // ----------------------------------
        private async void PhotoButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                //SaveToAlbum = false,
                //Directory = "Test",
                //Name = "test.jpg",
                //CompressionQuality = 75,
                //PhotoSize = PhotoSize.MaxWidthHeight,
                //MaxWidthHeight = 2000,
                //PhotoSize = PhotoSize.Custom,
                //CustomPhotoSize = 50,
                //DefaultCamera = CameraDevice.Front,
                //AllowCropping = true
            });


            if (photo == null)
                return;

            //------------------------------------------------------------------------------------------------------------------------------------------------

            // using app storage to store image
            var jpgCount = Directory.GetFiles(Path.GetDirectoryName(photo.Path)).Where(path => path.Contains(".jpg")).Count();
            var msg = $"{nameof(photo.Path)} = {photo.Path}\n" +
                $"Jpegs in app dir = {jpgCount}\n";

            //await DisplayAlert("blabla?", msg, "blabla.");

            PhotoPath = photo.Path;

            //------------------------------------------------------------------------------------------------------------------------------------------------

            // ???
            // update local instance that belongs to items collection
            Context.WordPicturePath = PhotoPath;
            original.WordPicturePath = Context.WordPicturePath;

            //this.contextItem.WordPicturePath = PhotoPath;
            if (original.Id == 0)
            {
                // add to repository
                await App.Repo.AddItemAsync(original);

                // Add to items collection.
                // Since collection type (observcoll) implements notifycollectionchanged
                // any listeners of the event (collection bound controls (listview, ...))
                // will be notified and act accordingly (display the new item)
                Words.Add(original);
            }
            // already existing item
            else
            {
                // update in repository
                await App.Repo.UpdateItemAsync(original);
            }

            // Will navigate back to previous page.
            // temp object will be lost (garbage collected)
            await Navigation.PopAsync();
        }
    
    }
}