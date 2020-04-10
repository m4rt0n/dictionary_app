﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grid : ContentPage
    {
        public ObservableCollection<Word> Words { get; private set; }

        public Grid()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await App.Repo.GetAllItemsAsync();
        }

        async void OnPictureSelected(object sender, SelectedItemChangedEventArgs e)
        {


            var item = collectionView.SelectedItem as Word;
            if (item != null)
            {
                await Navigation.PushAsync(new WordDetailPage(item, Words));

            }


            /*
            private void OnPictureSelected(object sender, ItemTappedEventArgs e)
            {
                //SelectionChangedEventArgs

                Word tappedPic = e.Item as Word;
                if (tappedPic != null)
                {
                     Navigation.PushAsync(new WordDetailPage(tappedPic, Words));

                }
            }
            */

        }
    }
}