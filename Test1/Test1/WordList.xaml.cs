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
        public ObservableCollection<WordModel> collection { get; set; }

        public static List<WordModel> Items { get; private set; }

        public WordList()
        {
            InitializeComponent();



            //Items = new List<WordModel>();
            //listView.ItemsSource = Items;

            Items = new List<WordModel>();
            Task.Run(async () =>
            {
                List<WordModel> items = await App.Database.GetItemsAsync();
                foreach (var item in items) Items.Add(item);
            });


            collection = new ObservableCollection<WordModel>(Items);
            listView.ItemsSource = Items;

            //constructor for list items 
            /*
             * public string PhotoPath
        {
            get => _photoPath;
            set
            {
                _photoPath = value;
                OnPropertyChanged();
            }
        }
        */
            //List<Object> myList = new List<Objects>();
            //ObservableCollection<Object> myCollection = new ObservableCollection<Object>(myList);
            //GetList();
        }
        /*
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }
        */

            /*
             * 
             * List<Employee> employees = new List<Employee>();
             * ObservableCollection<Employee> sessions = new ObservableCollection<Employee>(employees);           

                 listView.SelectedItem.Clear();
                 ((ListView)sender).SelectedItem = null;
                 myListView.SelectedItem = null;

                 foreach (ListViewItem i in myListView.SelectedItems)
                 {
                     i.IsSelected = false;
                 }            
             
            */

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
            await DisplayAlert("Delete", item.WordEng, "OK");
            collection.Remove(item);


            //Items.Clear();
            //listView.ItemsSource = null;
            //Items.Remove(item);
            //GetList();
            //Items.Remove(item);
            //Items.Refresh();
            //listView.ItemsSource = null;
            //Items.Clear();
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
            Grid showGrid = new Grid();
            await Navigation.PushAsync(showGrid);
        }

        void Search_Clicked(object sender, EventArgs e)
        {
            var keyword = SearchBar.Text;
            listView.ItemsSource =           
            Items.Where(x => x.WordEng.ToLower().Contains(keyword.ToLower()) 
            || x.WordRus.ToLower().Contains(keyword.ToLower()));
        }

        void GetList()
        {
            Task.Run(async () =>
                {
                    List<WordModel> items = await App.Database.GetItemsAsync();
                    foreach (var item in items) Items.Add(item);
                });
        }

        /*
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        */
    }
}

/*
 public void RefreshData()
        {
            listView.ItemsSource = null;
            listView.ItemsSource = Items;
            GetList();
        }   
*/


