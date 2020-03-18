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



namespace Test1
{
    
    public partial class Photo : ContentPage, INotifyPropertyChanged
    {
        private ItemModel contextItem;

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

        ImageSource _photoSource;
        public ImageSource PhotoSource
        {
            get => _photoSource;
            set
            {
                _photoSource = value;
                OnPropertyChanged();
            }
        }

        public Photo(ItemModel photo)
        {
            InitializeComponent();
            this.contextItem = photo;
            //binaryData = photo.ImageData;

            BindingContext = this;
        }

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

            // v1: using app storage to store image
            var jpgCount = Directory.GetFiles(Path.GetDirectoryName(photo.Path)).Where(path => path.Contains(".jpg")).Count();
            var msg = $"{nameof(photo.Path)} = {photo.Path}\n" +
                $"Jpegs in app dir = {jpgCount}\n";

            //await DisplayAlert("blabla?", msg, "blabla.");

            PhotoPath = photo.Path;

            // v2: using database to store image
            string stringToBeSavedToDb = photo.GetStream().ConvertToBase64();
            byte[] binaryData = Convert.FromBase64String(stringToBeSavedToDb);
            ImageSource source = ImageSource.FromStream(() => new MemoryStream(binaryData));

            Debug.WriteLine($"---> DEBUG MESSAGE: {nameof(stringToBeSavedToDb)}.Count() = {stringToBeSavedToDb.Count()} <---");

            PhotoSource = source;

            //------------------------------------------------------------------------------------------------------------------------------------------------

            this.contextItem.ImageData = binaryData;
            if (this.contextItem.Id == 0)
            {
                await App.Database.AddItemAsync(this.contextItem);
                await DisplayAlert("photo added", this.contextItem.Id.ToString(), "OK");
            }
            else
            {
                await App.Database.UpdateItemAsync(this.contextItem);
                await DisplayAlert("photo updated", msg, "OK");
            }

        }
    }

    public static class ExtensionMethods
    {
        public static string ConvertToBase64(this Stream stream)
        {
            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }
    }
}
