using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grid : ContentPage
    {
        public Grid()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            collectionView.ItemsSource= await App.Database.GetItemsAsync();
        }
    }
}