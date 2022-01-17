using Shoppinglist.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shoppinglist.ViewModels
{
    public class BuildYourShopViewModel : BaseVM
    {
        StackLayout stack;
        BuildYourShopView bysView;
        string lab1_txt, lab2_txt, lab3_txt, lab4_txt, lab5_txt, lab6_txt, lab7_txt, lab8_txt, lab9_txt, lab10_txt;
        #region Property Change
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
        #endregion Property Change

        public BuildYourShopViewModel(INavigation navigation, StackLayout stack, BuildYourShopView bysView)
        {
            this.Navigation = navigation;
            this.stack = stack;
            this.bysView = bysView;
            Lab1_txt = "Drogerie";
            Lab2_txt = "Molkerei";
            Lab3_txt = "Obst & Gemüse";
            lab4_txt = "Trockensortiment";
            lab5_txt = "Getränke";
            lab6_txt = "Tee/Kaffee/Brot";
            lab7_txt = "Süßwaren";
            lab8_txt = "Tiefkühlwaren";
            lab9_txt = "Wurst & Käse";
            lab10_txt = "Tiernahrung";
        }
    }
}
