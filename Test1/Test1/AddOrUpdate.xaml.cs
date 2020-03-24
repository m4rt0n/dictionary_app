
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrUpdate : ContentPage, INotifyPropertyChanged
    {
        private WordModel contextItem;

        public AddOrUpdate(WordModel addOrUpdateWord)
        {
            this.contextItem = addOrUpdateWord;

            InitializeComponent();
            pWordEngEntry.Text = addOrUpdateWord.WordEng;
            pWordRusEntry.Text = addOrUpdateWord.WordRus;
            pWordNoteEntry.Text = addOrUpdateWord.WordNote;
            pWordPhoto.Source = addOrUpdateWord.WordPicturePath;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            this.contextItem.WordEng = (pWordEngEntry.Text);
            this.contextItem.WordRus = (pWordRusEntry.Text);
            this.contextItem.WordNote = (pWordNoteEntry.Text);

            if (this.contextItem.Id == 0)
            {               
                await App.Database.AddItemAsync(this.contextItem);
            }
            else
            {                
                await App.Database.UpdateItemAsync(this.contextItem);
            }
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        async void Photo_Clicked(object sender, EventArgs e)
        {           
            var item = this.contextItem;
            await Navigation.PushAsync(new Photo(item as WordModel));            
        }
    }
}