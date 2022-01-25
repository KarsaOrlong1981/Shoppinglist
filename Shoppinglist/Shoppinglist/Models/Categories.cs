using Shoppinglist.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shoppinglist.Models
{
    public class Categories
    {
        private Grid grid, categorieGrid;
        private MyShop myShop;
        private MainPage mainPage;
        private static List<ShopItems> strikedItemList, defaultList, drogerie_List, kaffee_List, snacks_List, teig_Trocken_List, tiernahrung_List, getraenke_List, tiefkuehl_List, fleisch_List, milch_list, obst_List, sonstiges_List;
        private SavedLists savedLists;
        private MainPageViewModel mainPageViewModel;
        private ListView listItemView, listViewStrikedItem;
       
        
        public Categories(Grid grid, MyShop myShop, MainPage mainPage, SavedLists savedLists, MainPageViewModel mainPageViewModel)
        {
            this.grid = grid;
            this.myShop = myShop;
            this.mainPage = mainPage;
            defaultList = new List<ShopItems>();    
            strikedItemList = new List<ShopItems>();
            drogerie_List = new List<ShopItems>();
            kaffee_List = new List<ShopItems>();
            snacks_List = new List<ShopItems>();
            teig_Trocken_List = new List<ShopItems>();
            tiernahrung_List = new List<ShopItems>();
            getraenke_List = new List<ShopItems>();
            tiefkuehl_List = new List<ShopItems>();
            fleisch_List = new List<ShopItems>();
            milch_list = new List<ShopItems>();
            obst_List = new List<ShopItems>();
            sonstiges_List = new List<ShopItems>();
            this.savedLists = savedLists;
            this.mainPageViewModel = mainPageViewModel;
            
            grid.Children.Clear();
        }
        public static void ClearAllLists()
        {
            defaultList.Clear();
            strikedItemList.Clear();
            drogerie_List.Clear();
            kaffee_List.Clear();
            snacks_List.Clear(); 
            teig_Trocken_List.Clear();
            tiernahrung_List.Clear();
            getraenke_List.Clear();
            tiefkuehl_List.Clear();
            fleisch_List.Clear();
            milch_list.Clear();
            obst_List.Clear();
            sonstiges_List.Clear();
        }
        private void NewCategorieGrid()
        {
            ScrollView scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                HeightRequest = 300
            };
            Grid categorieGrid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                 new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                  new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                   new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                     new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                      new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                       new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                        new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                         new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                          new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },

            },
            };
            this.categorieGrid = categorieGrid;
            Grid.SetRow(scrollView, 1);
            scrollView.Content = categorieGrid;
            grid.Children.Add(scrollView);
        }
        public async Task SetCategories(ShopItems shopItems)
        {
            grid.Children.Clear();
            NewCategorieGrid();
            await Task.Run(() =>
            AddItemsToCategorie());
            if (myShop.Name == "Keinen")
            {
                ListItemView(shopItems, savedLists.ListName, "", 0);
            }
            else
            {
                ListItemView(shopItems, savedLists.ListName, myShop.Lab1, 0);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab2, 1);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab3, 2);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab4, 3);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab5, 4);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab6, 5);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab7, 6);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab8, 7);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab9, 8);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab10, 9);
                ListItemView(shopItems, savedLists.ListName, myShop.Lab11, 10);
            }
            
            ListStrikedItems(shopItems);
        }
        private void AddItemsToCategorie()
        {
            //die items den listen zuweisen
            
            LoadListFromDB("Drogerie", savedLists.Tag);
            LoadListFromDB("Milchprodukte", savedLists.Tag);
            LoadListFromDB("Tee/Kaffee/Brot", savedLists.Tag);
            LoadListFromDB("Snacks", savedLists.Tag);
            LoadListFromDB("Teig-/Trockenwaren", savedLists.Tag);
            LoadListFromDB("Tiernahrung", savedLists.Tag);
            LoadListFromDB("Getränke", savedLists.Tag);
            LoadListFromDB("Tiefkühlwaren", savedLists.Tag);
            LoadListFromDB("Fleisch/Wurst/Fisch", savedLists.Tag);
            LoadListFromDB("Obst & Gemüse", savedLists.Tag);
            LoadListFromDB("Sonstiges", savedLists.Tag);
            LoadListFromDB("", savedLists.Tag);

        }
        private void ListItemView(ShopItems si,string title, string categorie, short row)
        {

            mainPage.Title = title;
            List<ShopItems> heigthRequestListCount = GetItemSource(categorie);
            if (heigthRequestListCount.Count > 0)
            {
                Grid gridStricked = new Grid
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                 new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
            },
                };
                Label lab_Categorie = new Label
                {
                    Text = categorie,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 25,
                    TextColor = Color.Magenta
                };

                listItemView = new ListView
                {
                    ItemsSource = GetItemSource(categorie),
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Always,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = GetListViewHeigth(heigthRequestListCount),
                    ItemTemplate = new DataTemplate(() =>
                    {
                        Label lab = new Label();
                        lab.SetBinding(Label.TextProperty, "ItemName");
                        lab.SetBinding(Label.TextDecorationsProperty, "Decorations");
                        lab.FontSize = 20.0;
                        lab.TextColor = Color.White;
                        lab.FontAttributes = FontAttributes.Bold;
                        lab.FontFamily = "AD";
                        lab.VerticalOptions = LayoutOptions.Center;
                        lab.HorizontalOptions = LayoutOptions.End;
                        lab.HorizontalTextAlignment = TextAlignment.Start;

                        si.ListCheckBox = new CheckBox();
                        si.ListCheckBox.SetBinding(CheckBox.IsCheckedProperty, "ListCBIsChecked");
                        si.ListCheckBox.SetBinding(CheckBox.ColorProperty, "ListCBColor");
                        si.ListCheckBox.IsEnabled = false;
                        if (si.ListCBIsChecked)
                        {
                            si.ListCBColor = Color.GreenYellow;
                            si.Decorations = TextDecorations.Strikethrough;
                        }
                        else
                        {
                            si.ListCBColor = Color.Default;
                            si.Decorations = TextDecorations.None;
                        }
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
                                                si.ListCheckBox,
                                                lab
                                            }
                                        }
                            }
                            }
                        };
                    })
                };
                // GetItemSource(categorie);

                listItemView.ItemTapped += ListItemView_ItemTapped;
                Grid.SetRow(lab_Categorie, 0);
                Grid.SetRow(listItemView, 1);
                gridStricked.Children.Add(lab_Categorie);
                gridStricked.Children.Add(listItemView);
                Grid.SetRow(gridStricked, row);
                categorieGrid.Children.Add(gridStricked);
               
            }
           

        }

        private double GetListViewHeigth(List<ShopItems> listCount)
        {
            double heigth = 0;
            int count = listCount.Count;   
            if (listCount == defaultList)
            {
                heigth = 300;
            }
            else
            if (count > 0)
            {
                if (count <= 10)
                {
                    heigth = 150;
                }
                if (count > 10)
                {
                    heigth = 200;
                }
            }
            else
            {
                heigth = 50;
            }
            

            return heigth;
        }

        private List<ShopItems> GetItemSource(string categorie)
        {
            switch (categorie)
            {
                case "Drogerie": return drogerie_List;
                case "Milchprodukte": return milch_list;
                case "Snacks": return snacks_List;
                case "Tiernahrung": return tiernahrung_List; 
                case "Tee/Kaffee/Brot": return kaffee_List;
                case "Getränke": return getraenke_List;
                case "Teig-/Trockenwaren": return teig_Trocken_List; 
                case "Fleisch/Wurst/Fisch": return fleisch_List; 
                case "Tiefkühlwaren":return tiefkuehl_List; 
                case "Obst & Gemüse": return obst_List;
                case "": return defaultList;
                default: return sonstiges_List; 
            }
        }

        private void ListStrikedItems(ShopItems si)
        {
            Grid gridStricked = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            listViewStrikedItem = new ListView
            {
                ItemsSource = strikedItemList,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Always,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 300.0,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ItemName");
                    lab.SetBinding(Label.TextDecorationsProperty, "Decorations");
                    lab.FontSize = 20.0;
                    lab.TextColor = Color.White;
                    lab.FontAttributes = FontAttributes.Bold;
                    lab.FontFamily = "AD";
                    lab.VerticalOptions = LayoutOptions.Center;
                    lab.HorizontalOptions = LayoutOptions.End;
                    lab.HorizontalTextAlignment = TextAlignment.Start;
                   
                    si.ListCheckBox = new CheckBox();
                    si.ListCheckBox.SetBinding(CheckBox.IsCheckedProperty, "ListCBIsChecked");
                    si.ListCheckBox.SetBinding(CheckBox.ColorProperty, "ListCBColor");
                    si.ListCheckBox.IsEnabled = false;
                    if (si.ListCBIsChecked)
                    {
                        si.ListCBColor = Color.GreenYellow;
                        si.Decorations = TextDecorations.Strikethrough;
                    }
                    else
                    {
                        si.ListCBColor = Color.Default;
                        si.Decorations = TextDecorations.None;
                    }
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
                                                si.ListCheckBox,
                                                lab
                                            }
                                        }
                            }
                        }
                    };
                })
            };
            listViewStrikedItem.ItemTapped += ListViewStrikedItem_ItemTapped; ;
            gridStricked.Children.Add(listViewStrikedItem);
            Grid.SetRow(gridStricked, 3);
            grid.Children.Add(gridStricked);
        }

        private void ListViewStrikedItem_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           // mainPageViewModel.StrikedItemsToDO(sender, listViewStrikedItem, myShop, savedLists);
            var itemData = (sender as ListView).SelectedItem as ShopItems;
            List<ShopItems> categorieList = new List<ShopItems>();
            switch (itemData.Typ)
            {
                case "Drogerie": categorieList = drogerie_List;break;
                case "Milchprodukte": categorieList = milch_list;break;
                case "Snacks": categorieList = snacks_List; break;
                case "Tiernahrung": categorieList = tiernahrung_List; break;
                case "Tee/Kaffee/Brot": categorieList = kaffee_List; break;
                case "Getränke": categorieList = getraenke_List; break;
                case "Teig-/Trockenwaren": categorieList = teig_Trocken_List; break;
                case "Fleisch/Wurst/Fisch": categorieList = fleisch_List; break;
                case "Tiefkühlwaren": categorieList = tiefkuehl_List; break;
                case "Obst & Gemüse": categorieList = obst_List; break;
                default: categorieList = sonstiges_List;break;
            }
            if (itemData.ListCBIsChecked == false)
            {
                itemData.ListCBIsChecked = true;

            }
            else
            {
                itemData.ListCBIsChecked = false;
                strikedItemList.Remove(itemData);
                if (!(categorieList.Contains(itemData)))
                {
                    bool hit = false;
                    itemData.ListCheckBox = new CheckBox();
                    itemData.ListCheckBox.IsChecked = false;
                    for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
                    {
                        if (App.Db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            hit = true;
                        }
                    }
                    if (hit == false)
                    {
                      mainPageViewModel.AddItemToDB(itemData.Tag, itemData.ListTag, itemData.ItemName, itemData.Typ);
                    }

                    for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
                    { // ein tag hinzufügen
                        if (App.Db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            ShopItems si = App.Db.GetAllItemsAsync().Result[i];
                            if (!(categorieList.Contains(si)))
                                categorieList.Add(si);
                        }
                    }
                }

            }
            listItemView.SelectedItem = null;
            ListStrikedItems(itemData);
            mainPageViewModel.SetInfoToDoDone();
        }

        private async void ListItemView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as ShopItems;
            switch (itemData.Typ)
            {
                case "Drogerie": RemoveItem(itemData, "Drogerie"); break;
                case "Milchprodukte": RemoveItem(itemData, "Milchprodukte"); break;
                case "Snacks": RemoveItem(itemData, "Snacks"); break;
                case "Tiernahrung": RemoveItem(itemData, "Tiernahrung"); break;
                case "Tee/Kaffee/Brot": RemoveItem(itemData,"Tee/Kaffee/Brot"); break;
                case "Getränke": RemoveItem(itemData, "Getränke"); break;
                case "Teig-/Trockenwaren": RemoveItem(itemData, "Teig-/Trockenwaren"); break;
                case "Fleisch/Wurst/Fisch": RemoveItem(itemData, "Fleisch/Wurst/Fisch"); break;
                case "Tiefkühlwaren": RemoveItem(itemData, "Tiefkühlwaren"); break;
                case "Obst & Gemüse": RemoveItem(itemData, "Obst & gemüse"); break;
                default: RemoveItem(itemData,"Sonstiges"); break;
            }
           
            listItemView.SelectedItem = null;

            // ListItemView(itemData.ListTag, itemData);

            await SetCategories(itemData);
            mainPageViewModel.SetInfoToDoDone();
            
        }

        private void RemoveItem(ShopItems itemData, string categorie)
        { 
           
            if (itemData.ListCBIsChecked == false)
            {
                itemData.ListCBIsChecked = true;
                switch (categorie)
                {
                    case "Drogerie": drogerie_List.Remove(itemData); break;
                    case "Milchprodukte": milch_list.Remove(itemData); break;
                    case "Snacks": snacks_List.Remove(itemData); break;
                    case "Tiernahrung": tiernahrung_List.Remove(itemData); break;
                    case "Teig-/Trockenwaren": teig_Trocken_List.Remove(itemData); break;
                    case "Fleisch/Wurst/Fisch": fleisch_List.Remove(itemData); break;
                    case "Tiefkühlwaren": tiefkuehl_List.Remove(itemData); break;
                    case "Obst & Gemüse": obst_List.Remove(itemData); break;
                    default: sonstiges_List.Remove(itemData); break;
                }
                
                itemData.ListCheckBox = new CheckBox();
                itemData.ListCheckBox.IsChecked = true;
                strikedItemList.Add(itemData);
                CallDelete(itemData.Id);
              
            }
            else
            {
                itemData.ListCBIsChecked = false;
                //AddItemToDB(itemData.ListName, itemData.ItemName);
            }
        }

        private void LoadListFromDB(string categorie, string listTag)
        {
            //hier darf immer nur die liste enthalten sein die aktuell gewählt wurde

            var db = App.Db;
            switch (categorie)
            {
                case "Drogerie": AddtoDrogerieList("Drogerie", listTag, db); break;
                case "Milchprodukte": AddtoMilchProdukteList("Milchprodukte", listTag, db); break;
                case "Snacks": AddtoSnacksList("Snacks", listTag,db); break;
                case "Tiernahrung": AddtoTiernahrungList("Tiernahrung", listTag, db); break;
                case "Tee/Kaffee/Brot": AddtoTee_KaffeeList("Tee/Kaffee/Brot",listTag, db); break;
                case "Getränke": AddtoGetraenkeList("Getränke", listTag, db); break;
                case "Teig-/Trockenwaren": AddtoTrockenwarenList("Teig-/Trockenwaren", listTag, db) ; break;
                case "Fleisch/Wurst/Fisch": AddtoFleischList("Fleisch/Wurst/Fisch", listTag, db); break;
                case "Tiefkühlwaren": AddtoTiefkuehlList("Tiefkühlwaren", listTag, db); break;
                case "Obst & Gemüse": AddtoObstList("Obst & Gemüse", listTag, db); break;
                case "": AddtoDefaultList(listTag, db); break;
                default: AddtoSonstigesList("Sonstiges", listTag, db); break;
            }
           
        }
        private void CallDelete(int id)
        {
            DeleteFromDB(id);
        }
        private async void DeleteFromDB(int id)
        {
           await App.Db.DeleteItemAsync(id);
        }
        #region Add to list
        private void AddtoDefaultList(string listTag, Database db)
        {
            defaultList.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        defaultList.Add(db.GetAllItemsAsync().Result[i]);
                    }
                }

            }
        }
        private void AddtoSonstigesList(string categorie, string listTag, Database db)
        {
            sonstiges_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            sonstiges_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoObstList(string categorie, string listTag, Database db)
        {
           obst_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            obst_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoTiefkuehlList(string categorie, string listTag, Database db)
        {
            tiefkuehl_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            tiefkuehl_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoFleischList(string categorie, string listTag, Database db)
        {
            fleisch_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            fleisch_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoTrockenwarenList(string categorie, string listTag, Database db)
        {
            teig_Trocken_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            teig_Trocken_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoGetraenkeList(string categorie, string listTag, Database db)
        {
            getraenke_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            getraenke_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoTee_KaffeeList(string categorie, string listTag, Database db)
        {
            kaffee_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                           kaffee_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoTiernahrungList(string categorie, string listTag, Database db)
        {
            tiernahrung_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            tiernahrung_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoDrogerieList(string categorie, string listTag, Database db)
        {
            drogerie_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            drogerie_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoMilchProdukteList(string categorie, string listTag, Database db)
        {
            milch_list.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            milch_list.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        private void AddtoSnacksList(string categorie, string listTag, Database db)
        {
            snacks_List.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            snacks_List.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
        }
        #endregion Add to List
    }
}
