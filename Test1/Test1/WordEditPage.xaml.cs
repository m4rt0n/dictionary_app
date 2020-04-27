using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordEditPage : ContentPage, INotifyPropertyChanged
    {
        private Word original;

        public Word Context
        {
            get
            {
                return this.BindingContext as Word;
            }
        }

        public ObservableCollection<Word> Words { get; set; }

        public WordEditPage(Word context, ObservableCollection<Word> words)
        {
            InitializeComponent();
            Words = words;
            original = context;

            var temp = new Word()
            {
                Id = original.Id,
                WordEng = original.WordEng,
                WordRus = original.WordRus,
                WordNote = original.WordNote,
                WordPicturePath = original.WordPicturePath
            };

            this.BindingContext = temp;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {

            original.Id = Context.Id;
            original.WordEng = Context.WordEng;
            original.WordRus = Context.WordRus;
            original.WordNote = Context.WordNote;
            original.WordPicturePath = Context.WordPicturePath;
            
            if (original.Id == 0)
            {
                await App.Repo.AddItemAsync(original);
                Words.Add(original);
            }
            else
            {
                await App.Repo.UpdateItemAsync(original);
            }
            await Navigation.PopAsync();
        }

        private void Photo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Photo(Context, Words));
        }
    }
}