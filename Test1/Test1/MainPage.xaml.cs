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
            Grid showGrid = new Grid(Words);
            Navigation.PushAsync(showGrid);           
        }


        void Search_Clicked(object sender, EventArgs e)
        {
            var keyword = SearchBar.Text;
            listView.ItemsSource =
            Words.Where(x => x.WordEng.ToLower().Contains(keyword.ToLower())
            || x.WordRus.ToLower().Contains(keyword.ToLower())).ToList();
        }
    }
}
