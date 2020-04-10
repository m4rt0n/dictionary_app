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
                // The binding context is the item set in the constructor.
                // It needs to be casted from 'Object' to 'Item'
                return this.BindingContext as Word;
            }
        }

        public ObservableCollection<Word> Words { get; set; }

        public WordEditPage(Word context, ObservableCollection<Word> words)
        {
            InitializeComponent();

            // save reference
            Words = words;

            // save reference to original
            original = context;

            // Create temporary as a copy to bind so that when temp changes
            // through binding, the original will not be modified.
            // If we wouldn't be using a temp object, changes would be applied to
            // locally even without pressing save
            var temp = new Word()
            {
                Id = original.Id,
                WordEng = original.WordEng
            };

            // the temp will be the bound object
            this.BindingContext = temp;
        }

        private async void toolSave_Clicked(object sender, EventArgs e)
        {
            // ---------!!!!!!!!!!--------------

            // update local instance that belongs to items collection
            original.WordEng = Context.WordEng;
            original.WordPicturePath = Context.WordPicturePath;

            // the original is a new item that doesnt belong anywhere yet
            if (original.Id == 0)
            {
                // add to repository
                await App.Repo.AddItemAsync(original);

                // Add to items collection.
                // Since collection type (observcoll) implements notifycollectionchanged
                // any listeners of the event (collection bound controls (listview, ...))
                // will be notified and act accordingly (display the new item)
                Words.Add(original);
                await DisplayAlert("New ID: ", original.Id.ToString(), "OK");
            }
            // already existing item
            else
            {
                // update in repository
                await App.Repo.UpdateItemAsync(original);
                await DisplayAlert("Updated ID: ", original.Id.ToString(), "OK");
            }

            // Will navigate back to previous page.
            // temp object will be lost (garbage collected)
            await Navigation.PopAsync();
        }
        private void toolPhoto_Clicked(object sender, EventArgs e)
        {
            // pass on the references
            Navigation.PushAsync(new Photo(Context, Words));
        }
    }
}