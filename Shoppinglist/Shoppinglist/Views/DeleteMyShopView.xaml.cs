using Shoppinglist.Models;
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
    public partial class DeleteMyShopView : ContentPage
    {
        public DeleteMyShopView()
        {
            InitializeComponent();
            DeleteShop delete = new DeleteShop(grid, frame);
            delete.SetShops();
        }
    }
}