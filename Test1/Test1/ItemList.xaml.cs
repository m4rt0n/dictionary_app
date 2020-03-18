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

        async void Del_Clicked(object sender, EventArgs e)
        {
            var item = listView.SelectedItem as ItemModel;
            await App.Database.DeleteItemAsync(item);           
        }
    }
}

/*
 * xaml -- ItemsSource="{Binding Items}" 
 * 
 ObservableCollection

 OnPropertyChanged
*/
