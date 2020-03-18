using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemList : ContentPage
    {
        public ItemList()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void AddOrUpdate_Clicked(object sender, EventArgs e)
        {
            //select to update
            var item = listView.SelectedItem as ItemModel;
            //add new
            if (item == null)
                item = new ItemModel();


            await Navigation.PushAsync(new AddOrUpdate(item as ItemModel));

        }
    }
}
/*
 * private void Button_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as Model;

            // or
            if (item == null)
                item = new Model();


            Navigation.PushAsync(new EditPage(item as Model, additemtolist));
            Navigation.PushAsync(new EditPage(item as Model, (item)=>Items.Add(item)));
        }

    async void Update_Clicked(object sender, EventArgs args)
        {
            WordListPage back = new WordListPage();

                var updateword = (Word)BindingContext;
                await App.Database.UpdateWordAsync(updateword);
            
            await Navigation.PushAsync(back);
        }

     async void Save_Clicked(object sender, EventArgs e)
        {
            
            var savedWord = (Word)BindingContext;            
            await App.Database.AddWordAsync(savedWord);
            await Navigation.PopAsync();
        }
*/