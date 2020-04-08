using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Test1
{
    public partial class App : Application
    {
        // these references are used throughout the app
        public static WordRepo Repo { get; private set; }
        public ObservableCollection<Word> Words { get; private set; }

        public App()
        {
            InitializeComponent();

            // instantiate dependencies
            Repo = new WordRepo();
            Words = new ObservableCollection<Word>();

            // pass on (currently empty) collection
            MainPage = new NavigationPage(new MainPage(Words));
        }

        protected override async void OnStart()
        {
            // add items to collection in async method
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