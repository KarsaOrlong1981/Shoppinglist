using Shoppinglist.Model;
using Shoppinglist.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Shoppinglist.ViewModels
{
    public class BuildYourShopViewModel : BaseVM
    {
        StackLayout stack;
        Entry shopName;
        MainPageViewModel mainPage;
        BuildYourShopView bysView;
        
        string lab1_txt, lab2_txt, lab3_txt, lab4_txt, lab5_txt, lab6_txt, lab7_txt, lab8_txt, lab9_txt, lab10_txt, lab11_txt;
        #region Property Change
        public ICommand ButtonAddShop { get; set; }
        public INavigation Navigation { get; set; }
        public string Lab1_txt 
        { 
            get => lab1_txt;
            set => SetProperty(ref lab1_txt, value);
        }
        public string Lab2_txt 
        { 
            get => lab2_txt;
            set => SetProperty(ref lab2_txt, value);
        }
        public string Lab3_txt
        {
            get => lab3_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab4_txt
        {
            get => lab4_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab5_txt
        {
            get => lab5_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab6_txt
        {
            get => lab6_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab7_txt
        {
            get => lab7_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab8_txt
        {
            get => lab8_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab9_txt
        {
            get => lab9_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab10_txt
        {
            get => lab10_txt;
            set => SetProperty(ref lab3_txt, value);
        }
        public string Lab11_txt
        {
            get => lab11_txt;
            set => SetProperty(ref lab11_txt, value);
        }
        #endregion Property Change

        public BuildYourShopViewModel(INavigation navigation, StackLayout stack, BuildYourShopView bysView, MainPageViewModel mainPage)
        {
            this.Navigation = navigation;
            this.stack = stack;
            this.bysView = bysView;
            this.mainPage = mainPage;
            lab1_txt = "Tee/Kaffee/Brot";
            lab2_txt = "Snacks";
            lab3_txt = "Teig-/Trockenwaren";
            lab4_txt = "Tiernahrung";
            lab5_txt = "Getränke";
            lab6_txt = "Drogerie";
            lab7_txt = "Tiefkühlwaren";
            lab8_txt = "Fleisch/Wurst/Fisch";
            lab9_txt = "Milchprodukte";
            lab10_txt = "Obst & Gemüse";
            lab11_txt = "Sonstiges";
           
            ButtonAddShop = new Command(SaveNewShop);
           
        }

        private void SaveNewShop()
        {
            stack.Children.Clear();
            shopName = mainPage.CreateEntry("Den namen des Shops eingeben", LayoutOptions.StartAndExpand, LayoutOptions.FillAndExpand, Color.White, Color.Default,Color.Black);
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
        }

        private void BtnSaveShop_Clicked(object sender, EventArgs e)
        {
           AddnewShopToDB();
        }

        private async void AddnewShopToDB()
        {
            string tag = GetTag();
            await App.DbBYS.AddToDBAsync(new Models.MyShop
            {
                Name = shopName.Text,
                Tag = tag,
                Lab1 = lab1_txt,
                Lab2 = lab2_txt,
                Lab3 = lab3_txt,
                Lab4 = lab4_txt,
                Lab5 = lab5_txt,
                Lab6 = lab6_txt,
                Lab7 = lab7_txt,
                Lab8 = lab8_txt,
                Lab9 = lab9_txt,
                Lab10 = lab10_txt,
                Lab11 = lab11_txt,
            });
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
            Navigation.RemovePage(bysView);
        }
    }
}
