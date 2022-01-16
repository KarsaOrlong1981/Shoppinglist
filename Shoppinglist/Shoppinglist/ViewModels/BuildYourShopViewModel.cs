using Shoppinglist.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shoppinglist.ViewModels
{
    public class BuildYourShopViewModel : BaseVM
    {
        Grid grid;
        BuildYourShopView bysView;
        public INavigation Navigation { get; set; }
        public BuildYourShopViewModel(INavigation navigation, Grid grid, BuildYourShopView bysView)
        {
            this.Navigation = navigation;
            this.grid = grid;
            this.bysView = bysView;

        }
    }
}
