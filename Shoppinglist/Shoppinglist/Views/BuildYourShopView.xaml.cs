using Shoppinglist.Model;
using Shoppinglist.Models;
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
        MyShop myShop;
        Entry shopName;
        MainPageViewModel mainPage;
        public BuildYourShopView(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            sendingLabel = new Label();
            this.Title = "Build your Shop";
            this.mainPage = mainPageViewModel;
            CreateToolBarItem("Shop Liste anzeigen", "cross.png"); 
           
            lab1.Text = "Tee/Kaffee/Brot";
            lab2.Text = "Süßigkeiten";
            lab3.Text = "Snacks";
            lab4.Text = "Teig-/Trockenwaren";
            lab5.Text = "Tiernahrung";
            lab6.Text = "Getränke";
            lab7.Text = "Drogerie";
            lab8.Text = "Tiefkühlwaren";
            lab9.Text = "Fleisch/Wurst/Fisch";
            lab10.Text = "Milchprodukte";
            lab11.Text = "Obst & Gemüse";
            lab12.Text = "Sonstiges";
            
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            var label = (sender as Element)?.Parent as Label;
            e.Data.Properties.Add("Text", label.Text);
            label.BackgroundColor = Color.FromHex("#86AC41");
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
        private void CreateToolBarItem(string txt, string filename)
        {
            ToolbarItem item = new ToolbarItem
            {
                Text = txt,
                IconImageSource = ImageSource.FromFile(filename),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };
            item.Clicked += Item_Clicked; ;
            this.ToolbarItems.Add(item);
        }

        private async void Item_Clicked(object sender, EventArgs e)
        {
           await CallDeleteMyShop();
        }

        private void SaveNewShop()
        {

            stack.Children.Clear();
            shopName = mainPage.CreateEntry("Den namen des Shops eingeben", LayoutOptions.StartAndExpand, LayoutOptions.FillAndExpand, Color.White, Color.Black, Color.Black);
            Button btnSaveShop = new Button
            {
                Text = "OK",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20.0,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
            };
            btnSaveShop.Clicked += BtnSaveShop_Clicked;
            stack.Children.Add(shopName);
            stack.Children.Add(btnSaveShop);
            shopName.Focus();
        }

        private async void BtnSaveShop_Clicked(object sender, EventArgs e)
        {
            await AddnewShopToDB();
        }

        private async Task AddnewShopToDB()
        {

            string tag = GetTag();
            myShop = new MyShop
            {
                Name = shopName.Text,
                Tag = tag,
                Lab1 = lab1.Text,
                Lab2 = lab2.Text,
                Lab3 = lab3.Text,
                Lab4 = lab4.Text,
                Lab5 = lab5.Text,
                Lab6 = lab6.Text,
                Lab7 = lab7.Text,
                Lab8 = lab8.Text,
                Lab9 = lab9.Text,
                Lab10 = lab10.Text,
                Lab11 = lab11.Text,
                Lab12 = lab12.Text
            };
            await App.DbBYS.AddToDBAsync(myShop);

            WriteToast.ShowLongToast(shopName.Text + " wurde gespeichert");
            await CallMainPage();
        }

        private string GetTag()
        {
            DateTime dt = DateTime.Now;
            string tag = shopName.Text + dt.Year + dt.Month + dt.Day + dt.Hour + dt.Minute + dt.Second + dt.Millisecond;
            return tag;
        }

        private async Task CallMainPage()
        {
            MainPage call = new MainPage();
            await Navigation.PushAsync(call);
            Navigation.RemovePage(this);
        }
        private async Task CallDeleteMyShop()
        {
            DeleteMyShopView call = new DeleteMyShopView();
            await Navigation.PushAsync(call);
            Navigation.RemovePage(this);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SaveNewShop();
        }
    }
}