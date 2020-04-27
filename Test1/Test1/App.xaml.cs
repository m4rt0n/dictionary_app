using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Test1
{
    public partial class App : Application
    {       
        public static WordRepo Repo { get; private set; }
        public ObservableCollection<Word> Words { get; private set; }

        public App()
        {
            InitializeComponent();
            Repo = new WordRepo();
            Words = new ObservableCollection<Word>();
            MainPage = new NavigationPage(new MainPage(Words));
        }

        protected override async void OnStart()
        {
            List<Word> items = await Repo.GetAllItemsAsync();
            foreach (var item in items) Words.Add(item);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}