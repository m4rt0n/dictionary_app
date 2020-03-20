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
    public partial class Profile : ContentPage
    {
        private ItemModel contextItem;

        public Profile(ItemModel item)
        {
            this.contextItem = item;

            InitializeComponent();

            Pname.Text = item.Name;
            Pnote.Text = item.Note;
            Ppath.Source = item.PicturePath;
        }
    }
}