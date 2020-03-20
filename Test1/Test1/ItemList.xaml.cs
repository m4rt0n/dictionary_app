using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemList : ContentPage
    {
       
        public ItemList()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetItemsAsync();

        }

        async void AddOrUpdate_Clicked(object sender, EventArgs e)
        {
            //select to update
            var item = listView.SelectedItem as ItemModel;
            //add new
            if (item == null)
                item = new ItemModel();

            await Navigation.PushAsync(new AddOrUpdate(item as ItemModel));
        }

        async void Del_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as ItemModel;
            await App.Database.DeleteItemAsync(item);           
        }

        async void Photo_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as ItemModel;
            
            await Navigation.PushAsync(new Photo(item as ItemModel));                       
        }

        async void Profile_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as ItemModel;
            await Navigation.PushAsync(new Profile(item as ItemModel));
        }


        /*
       private ItemModel contextItem;

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
      
        ImageSource source = ImageSource.FromStream(() => new MemoryStream(this.contextItem.ImageData));
        PhotoSource = source;
        */

    }
}

/* 
 ObservableCollection

 OnPropertyChanged
*/
