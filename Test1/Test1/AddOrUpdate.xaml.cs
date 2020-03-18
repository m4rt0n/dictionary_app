
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrUpdate : ContentPage
    {
        private ItemModel contextItem;

        public AddOrUpdate(ItemModel item)
        {
            this.contextItem = item;

            InitializeComponent();
            nameEntry.Text = item.Name;
            noteEntry.Text = item.Note;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            this.contextItem.Name = (nameEntry.Text);
            this.contextItem.Note = (noteEntry.Text);

            if (this.contextItem.Id == 0)
            {               
                await App.Database.AddItemAsync(this.contextItem);
            }
            else
            {                
                await App.Database.UpdateItemAsync(this.contextItem);
            }
        }
    }
}