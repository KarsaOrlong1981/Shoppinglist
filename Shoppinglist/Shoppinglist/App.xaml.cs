using Shoppinglist.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shoppinglist
{
    public partial class App : Application
    {
        public static NavigationPage NavPage { get; set; }
        private static Database db;
        public static Database Db
        {
            get
            {
                if (db == null)
                {
                    db= new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db2"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();
            NavPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.White, BarTextColor = Color.Black }; 
            MainPage = NavPage;

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
