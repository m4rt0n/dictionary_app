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
    public partial class WordDetailPage : ContentPage, INotifyPropertyChanged
    {
        public Word Context
        {
            get
            {
                return this.BindingContext as Word;
            }
        }

        public ObservableCollection<Word> Words { get; set; }

        public bool CanDelete
        {
            get
            {
                return Context.Id != 0;
            }
        }

        public WordDetailPage(Word context, ObservableCollection<Word> words)
        {
            InitializeComponent();
            this.BindingContext = context;
            this.Words = words;
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WordEditPage(Context, Words));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Delete", "Sure?", "Yes", "No");

            if (result)
            {
                await App.Repo.DeleteItemAsync(Context);
                Words.Remove(Context);
                await Navigation.PopAsync();
            }
        }        
    }
}