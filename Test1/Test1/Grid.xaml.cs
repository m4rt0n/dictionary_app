using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Test1.ViewModels;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grid : ContentPage
    {
        public Grid()
        {
            this.BindingContext = new WordViewModel();

            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            collectionView.ItemsSource= await App.Database.GetItemsAsync();
        }

        

       async void OnPictureSelected(object sender, SelectedItemChangedEventArgs e)
       {
            //SelectionChangedEventArgs

           var item = collectionView.SelectedItem as WordModel;
           if (item != null)
           {
                await Navigation.PushAsync(new Profile(item as WordModel));
               
           }

           
        }

        /*
       


       */

    }
}