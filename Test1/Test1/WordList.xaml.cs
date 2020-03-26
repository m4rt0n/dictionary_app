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
    public partial class WordList : ContentPage
    {
       
        public WordList()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetItemsAsync();

            /*
            listView.SelectedItem.Clear();
            ((ListView)sender).SelectedItem = null;
            myListView.SelectedItem = null;

            foreach (ListViewItem i in myListView.SelectedItems)
            {
                i.IsSelected = false;
            }

            */

        }
        

        async void AddOrUpdate_Clicked(object sender, EventArgs e)
        {
           
            var item = listView.SelectedItem as WordModel;            
            if (item == null)
                item = new WordModel();

            await Navigation.PushAsync(new AddOrUpdate(item as WordModel));
            
        }

        async void Del_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as WordModel;
            await App.Database.DeleteItemAsync(item);           
        }
      
        async void Profile_Clicked(object sender, EventArgs e)
        {          
            var item = listView.SelectedItem as WordModel;

            if (item == null)
            { await DisplayAlert("no item selected", "tap to select", "OK"); }           
            else
            { await Navigation.PushAsync(new Profile(item as WordModel)); }
            
        }

        async void Grid_Clicked(object sender, EventArgs e)
        {
            Grid x = new Grid();
            await Navigation.PushAsync(x);
        }
    }
}

