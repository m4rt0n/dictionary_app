using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;

namespace Test1.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        private WordModel wordmodel;
        public ObservableCollection<WordModel> Words { get; set; }
        public static List<WordModel> Items { get; private set; }

        public ListViewModel()
        {
            Words = new ObservableCollection<WordModel>(List)
            {
                //--------?????--------
            Task.Run(async () =>
            {
                List<WordModel> items = await App.Database.GetItemsAsync();
                foreach (var item in items) Items.Add(item);
            });
        
        }

        

       

    }
}

