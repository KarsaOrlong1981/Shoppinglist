using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shoppinglist.Models
{
    public class DeleteShop
    {
        private List<MyShop> myShopNames;
        Grid grid;
        Frame frame;
      
        public DeleteShop(Grid grid, Frame frame)
        {
            myShopNames = new List<MyShop>();
            this.grid = grid;
            this.frame = frame;
        }
        public void SetShops()
        {  
            GetShopNames();
        }
        private void MyShopList()
        {
            if (myShopNames.Count == 0)
            {
                grid.Children.Clear();
                frame.IsVisible = false;
                Label label = new Label
                {
                    Text = "Es wurde noch kein Shop erstellt.",
                    FontFamily = "AD",
                    FontSize = 25,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start

                };
                Grid.SetRow(label, 1);
                grid.Children.Add(label);
            }
            else
            {
                ListView listMyShops = new ListView
                {
                    ItemsSource = myShopNames,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        Image img = new Image();
                        img.Source = "this24.png";
                        img.VerticalOptions = LayoutOptions.CenterAndExpand;
                        img.HorizontalOptions = LayoutOptions.StartAndExpand;

                        Label lab = new Label();
                        lab.SetBinding(Label.TextProperty, "Name");

                        lab.FontSize = 20.0;
                        lab.TextColor = Color.FromHex("#86AC41");
                        lab.FontAttributes = FontAttributes.Bold;
                        lab.FontFamily = "AD";
                        lab.VerticalOptions = LayoutOptions.Center;
                        lab.HorizontalOptions = LayoutOptions.End;
                        lab.HorizontalTextAlignment = TextAlignment.Start;
                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                    {
                                        new StackLayout
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Spacing = 0,

                                            Children =
                                            {
                                                img,
                                                lab
                                            }
                                        }
                            }
                            }
                        };
                    })
                };
                listMyShops.ItemTapped += ListMyShops_ItemTapped; ;

                Grid.SetRow(listMyShops, 1);
                grid.Children.Add(listMyShops);
            }
           
        }
        private void GetShopNames()
        {
            myShopNames.Clear();
            var dbBYS = App.DbBYS;
           

            if (dbBYS.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < dbBYS.GetDBCount().Result; i++)
                {
                    myShopNames.Add(dbBYS.GetAllItemsAsync().Result[i]);
                }

            }
            MyShopList();
        }
        private async Task DeleteListFromDB(string shopTag)
        {

            // Die liste selbst löschen
            var dbBYS = App.DbBYS;
            if (dbBYS.GetAllItemsAsync().Result.Count > 0)
            {
                int id = 0;
                for (int i = 0; i < dbBYS.GetDBCount().Result; i++)
                {
                    if (dbBYS.GetAllItemsAsync().Result[i].Tag == shopTag)
                    {
                        id = dbBYS.GetAllItemsAsync().Result[i].Id;
                        await dbBYS.DeleteItemAsync(id);
                    }
                }
            }
           
        }
        private async void ListMyShops_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myShop = (sender as ListView).SelectedItem as MyShop;
            await DeleteListFromDB(myShop.Tag);
            (sender as ListView).SelectedItem = null;
            (sender as ListView).ItemsSource = null;
            SetShops();
        }
    }
}
