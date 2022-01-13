﻿using Shoppinglist.Models;
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
        private static DatabaseLists dbLists;
        public static Database Db
        {
            get
            {
                if (db == null)
                {
                    db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db4"));
                }
                return db;
            }
        }
        public static DatabaseLists DbLists
        {
            get
            {
                if (dbLists == null)
                {
                    dbLists = new DatabaseLists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Listen.db2"));
                }
                return dbLists;
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
