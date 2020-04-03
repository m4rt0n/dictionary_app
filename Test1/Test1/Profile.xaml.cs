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
        private WordModel contextItem;

        public Profile(WordModel wordProfile)
        {
            this.contextItem = wordProfile;

            InitializeComponent();

            profWordPicturePath.Source = wordProfile.WordPicturePath;
            profWordEng.Text = wordProfile.WordEng;
            profWordRus.Text = wordProfile.WordRus;
            profWordNote.Text = wordProfile.WordNote;           
        }
       
    }
}