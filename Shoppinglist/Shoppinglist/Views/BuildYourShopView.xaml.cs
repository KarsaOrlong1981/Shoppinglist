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
        public BuildYourShopView()
        {
            InitializeComponent();
            BindingContext = new BuildYourShopViewModel(Navigation, grid, this);
        }
    }
}