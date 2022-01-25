using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shoppinglist
{
    public class ItemTyp
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
       
        public ItemTyp()
        {
            ReadAllAssets();   
        }
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
        private static void GetKaffee_BrotItems()
        {
            ReadAsset("Kaffe_Tee_Brot.txt", kaffee_Tee_Brot_List);
        }
        private static void GetSnacksItems()
        {
            ReadAsset("Snacks.txt", snacks_List);
        }
        private static void GetTeig_TrockenItems()
        {
            ReadAsset("Teig_Trockenwaren.txt", teig_Trockenware_List);
        }
        private static void GetTiernahrungItems()
        {
            ReadAsset("Tiernahrung.txt", tiernahrung_List);
        }
        private static void GetGetraenkeItems()
        {
            ReadAsset("Getraenke.txt", getraenke_list);
        }
        private static void GetTiefkuehlItems()
        {
            ReadAsset("Tiefkuehlwaren.txt", tiefkuehl_list);
        }
        private static void GetFleischItems()
        {
            ReadAsset("Fleisch_Wurst_Fisch.txt", fleisch_Wurst_Fisch_List);
        }
        private static void GetObstItems()
        {
            ReadAsset("Obst_Gemuese.txt", obst_gemuese_List);
        }
        public string GetCategorieTyp(string itemName)
        {
            string typ = "Sonstiges";
            itemName = itemName.ToUpper();
           
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
            foreach (var item in getraenke_list)
            {
                if (itemName.Contains(item))
                {
                    typ = "Getränke";
                    break;
                }
            }
            foreach (var item in obst_gemuese_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Obst & Gemüse";
                    break;
                }
            }
            foreach (var item in snacks_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Snacks";
                    break;
                }
            }
            foreach (var item in teig_Trockenware_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Teig-/Trockenwaren";
                    break;
                }
            }
            foreach (var item in tiernahrung_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Tiernahrung";
                    break;
                }
            }
            foreach (var item in fleisch_Wurst_Fisch_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Fleisch/Wurst/Fisch";
                    break;
                }
            }
            foreach (var item in kaffee_Tee_Brot_List)
            {
                if (itemName.Contains(item))
                {
                    typ = "Tee/Kaffee/Brot";
                    break;
                }
            }
            foreach (var item in tiefkuehl_list)
            {
                if (itemName.Contains(item))
                {
                    typ = "Tiefkühlwaren";
                    break;
                }
            }
           
            return typ;
        }
       
        private static void ReadAllAssets()
        {
            GetDrogerieItems();
            GetMilchprodukteItems();
            GetFleischItems();
            GetGetraenkeItems();
            GetKaffee_BrotItems();
            GetObstItems();
            GetSnacksItems();
            GetTeig_TrockenItems();
            GetTiefkuehlItems();
            GetTiernahrungItems();
        }
    }
}
