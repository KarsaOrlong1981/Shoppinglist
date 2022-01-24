using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shoppinglist
{
    public static class ItemTyp
    {
        private static List<string> drogerie_List = new List<string>();
        private static List<string> kaffee_Tee_Brot_List = new List<string>();
        private static List<string> snacks_List = new List<string>();
        private static List<string> teig_Trockenware_List = new List<string>();
        private static List<string> tiernahrung_List = new List<string>();
        private static List<string> getraenke_list = new List<string>();
        private static List<string> tiefkuehl_list = new List<string>();
        private static List<string> fleisch_Wurst_Fisch_List = new List<string>();
        private static List<string> milchprodukte_List = new List<string>();
        private static List<string> obst_gemuese_List = new List<string>();
        private static List<string> sonstiges_list = new List<string>();

        private static void ReadAsset(string filename, List<string> list)
        {
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader reader = new StreamReader(assets.Open(filename)))
            {
                while (!(reader.EndOfStream))
                {
                    string line = reader.ReadLine();
                    
                    list.Add(line.ToUpper());
                }
            }
        }
        private static void GetDrogerieItems()
        {
            ReadAsset("DrogerieItems.txt", drogerie_List);
            
        }
        private static void GetMilchprodukteItems()
        {
            ReadAsset("MilchprodukteItems.txt", milchprodukte_List);
        }
        public static string GetCategorieTyp(string itemName)
        {
            string typ = "Sonstiges";
            itemName = itemName.ToUpper(); 
            GetDrogerieItems();
            GetMilchprodukteItems();
            foreach (var item in drogerie_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Drogerie";
                    break;
                }
            }

            foreach (var item in milchprodukte_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Milchprodukte";
                    break;
                }
            }
           

            return typ;
        }
    }
}
