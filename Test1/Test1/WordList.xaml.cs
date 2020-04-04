using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
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
    public partial class WordList : ContentPage, INotifyPropertyChanged
    {
        

        public static List<WordModel> Items { get; private set; }

        

        public WordList()
        {
            InitializeComponent();
            
            //listView.ItemsSource = Items;
            
            GetList();
                                 
            listView.RefreshCommand = new Command(() => {               
                RefreshData();
                listView.IsRefreshing = false;
            });

            /*

                 ListView.SelectedItem = null;
                 listView.SelectedItem.Clear();

                 ((ListView)sender).SelectedItem = null;


                 foreach (ListViewItem i in myListView.SelectedItems)
                 {
                     i.IsSelected = false;
                 }                       
            */
        }

        /*
        ItemTapped="Tapped" 
         
        void Tapped(object sender, ItemTappedEventArgs e)
        {
            //var tapped = listView.ItemTapped;

            var selected = listView.SelectedItem;
            var tapped = e.Item;
            if (selected != null && tapped == selected) listView.SelectedItem = null;

            //if (tapped==selected)
        }

            */

        /*
          ItemSelected="OnItemSelected"
         
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender != null && sender is ListView listview)
            {
                if (e != null) listView.SelectedItem = null;
            }
        }
        ----------------
        
        */

        async void AddOrUpdate_Clicked(object sender, EventArgs e)
            {

                var item = listView.SelectedItem as WordModel;
                if (item == null)
                    item = new WordModel();
                await Navigation.PushAsync(new AddOrUpdate(item as WordModel));
                listView.SelectedItem = null;
        }
        
            async void Del_Clicked(object sender, EventArgs e)
            {
                var item = listView.SelectedItem as WordModel;
                await App.Database.DeleteItemAsync(item);
                await DisplayAlert("Delete", item.WordEng, "OK");                              
        }

            async void Profile_Clicked(object sender, EventArgs e)
            {
                var item = listView.SelectedItem as WordModel;

                if (item == null)
                { await DisplayAlert("no item selected", "tap to select", "OK"); }
                else
                { await Navigation.PushAsync(new Profile(item as WordModel)); }
            listView.SelectedItem = null;
        }

            async void Grid_Clicked(object sender, EventArgs e)
            {
                Grid showGrid = new Grid();
                await Navigation.PushAsync(showGrid);
                listView.SelectedItem = null;
        }

            void Search_Clicked(object sender, EventArgs e)
            {
                var keyword = SearchBar.Text;
                listView.ItemsSource =
                Items.Where(x => x.WordEng.ToLower().Contains(keyword.ToLower())
                || x.WordRus.ToLower().Contains(keyword.ToLower()));
            }

            public void GetList()
            {
            Items = new List<WordModel>();
            Task.Run(async () =>
                    {
                        List<WordModel> items = await App.Database.GetItemsAsync();
                        foreach (var item in items) Items.Add(item);
                    });
                listView.ItemsSource = Items;
            }

        public void RefreshData()
        {

            listView.ItemsSource = null;
            GetList();
            listView.ItemsSource = Items;
        }
    }
    }


/*
 
*/


