using Shoppinglist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shoppinglist.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuildYourShopView : ContentPage
    {
        Label sendingLabel;
        public BuildYourShopView(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            sendingLabel = new Label();
            BindingContext = new BuildYourShopViewModel(Navigation, stack, this, mainPageViewModel);
           
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            var label = (sender as Element)?.Parent as Label;
            e.Data.Properties.Add("Text", label.Text);
            label.BackgroundColor = Color.Green;
            sendingLabel = label;
        }

        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            var data = e.Data.Properties["Text"].ToString();

            //e.Data.GetTextAsync();
            var label = (sender as Element)?.Parent as Label;
            label.BackgroundColor = Color.White;
            sendingLabel.Text = "";
            sendingLabel.Text = label.Text;
            sendingLabel.BackgroundColor = Color.White;
            label = new Label
            {
                Text = data
            };
        }
    }
}