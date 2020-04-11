using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test1
{

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Word> Words { get; private set; }

        public MainPage(ObservableCollection<Word> words)
        {
            InitializeComponent();
            Words = words;
            // BindingContext = Items; could be used but in that case
            // ItemsSource="{Binding}" needs to be used for the listView 
            // and every bound property will be looked for there (in the observcoll) 
            // (eg command bindings).
            // When a viewmodel is used, it could inherit from ObservableCollection
            // and further properties could be declared there:
            // class ViewModel : ObservableCollection<Item> { }
            // this.BindingContext = new ViewModel()
            BindingContext = this;
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Word context = e.Item as Word;
            Navigation.PushAsync(new WordDetailPage(context, Words));
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            Word context = new Word();
            Navigation.PushAsync(new WordEditPage(context, Words));
        }

        private void Grid_Clicked(object sender, EventArgs e)
        {
            Grid showGrid = new Grid();
            Navigation.PushAsync(showGrid);           
        }


        void Search_Clicked(object sender, EventArgs e)
        {
            var keyword = SearchBar.Text;
            listView.ItemsSource =
            Words.Where(x => x.WordEng.ToLower().Contains(keyword.ToLower())
            || x.WordRus.ToLower().Contains(keyword.ToLower()));
        }
    }
}
