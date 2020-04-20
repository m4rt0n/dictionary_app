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

        // can only delete the item if it exists in repo and/or collection
        // if id is 0, it is a new item
        public bool CanDelete
        {
            get
            {
                return Context.Id != 0;
            }
        }

        // Passing reference to context item and the reference to collection.
        // Since the collection is passed, the ID of the context 
        // item would be enough here instead of the item itself.
        public WordDetailPage(Word context, ObservableCollection<Word> words)
        {
            InitializeComponent();
            this.BindingContext = context;
            this.Words = words;
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            // pass on the references
            Navigation.PushAsync(new WordEditPage(Context, Words));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            // propt user
            var result = await DisplayAlert("Delete", "Sure?", "Yes", "No");

            if (result)
            {
                // delete from repository
                await App.Repo.DeleteItemAsync(Context);
                // delete from items collection
                Words.Remove(Context);

                await Navigation.PopAsync();
            }
        }

        
    }
}