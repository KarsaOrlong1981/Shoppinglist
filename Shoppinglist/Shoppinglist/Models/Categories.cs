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
        private ShopItems shopItem;
        private List<ShopItems> strikedItemList, defaultList, suessikeiten_List, drogerie_List, kaffee_List
                              , snacks_List, teig_Trocken_List, tiernahrung_List, getraenke_List, tiefkuehl_List, fleisch_List
                              , milch_list, obst_List, sonstiges_List, allEmptyList;
        private SavedLists savedLists;
        private MainPageViewModel mainPageViewModel;
        private ListView listItemView, listViewStrikedItem;
        private List<Label> categorieLabels;
        private List<Grid> categorieGrids;
        private List<ShopItems>[] allLists;
        private bool isAllListEmpty;
        public Categories(Grid grid, MyShop myShop, MainPage mainPage, SavedLists savedLists, MainPageViewModel mainPageViewModel)
        {
            this.grid = grid;
            this.myShop = myShop;
            this.mainPage = mainPage;
            defaultList = new List<ShopItems>();    
            strikedItemList = new List<ShopItems>();
            drogerie_List = new List<ShopItems>();
            kaffee_List = new List<ShopItems>();
            suessikeiten_List = new List<ShopItems>();
            snacks_List = new List<ShopItems>();
            teig_Trocken_List = new List<ShopItems>();
            tiernahrung_List = new List<ShopItems>();
            getraenke_List = new List<ShopItems>();
            tiefkuehl_List = new List<ShopItems>();
            fleisch_List = new List<ShopItems>();
            milch_list = new List<ShopItems>();
            obst_List = new List<ShopItems>();
            sonstiges_List = new List<ShopItems>();
            allEmptyList = new List<ShopItems>();
            categorieLabels = new List<Label>();
            categorieLabels.Clear();
            categorieGrids = new List<Grid>();
            categorieGrids.Clear();
            this.savedLists = savedLists;
            this.mainPageViewModel = mainPageViewModel;
            isAllListEmpty = false;
            grid.Children.Clear();
        }
        #region Methods
      
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
            await Task.Run(() => AddItemsToCategorie());
           
            if (IsAllListEmpty())
            {
                shopItem = new ShopItems { ItemName = "Keine Einträge mehr vorhanden." };
                allEmptyList.Add(shopItem);
                isAllListEmpty = true;
               
            }
            if (myShop.Name == "Keinen" || isAllListEmpty == true)
            {
                switch (isAllListEmpty)
                {
                    case true: ListItemView(shopItems, savedLists.ListName, "Leere Liste", 0); break;
                    case false: ListItemView(shopItems, savedLists.ListName, "", 0); break;
                }
                
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
                ListItemView(shopItems, savedLists.ListName, myShop.Lab12, 11);
            }
            
            ListStrikedItems(shopItems);
        }
        private void AddItemsToCategorie()
        {
            //die items den listen zuweisen
            LoadListFromDB("Süßigkeiten", savedLists.Tag);
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
            allLists = new List<ShopItems>[] { strikedItemList, drogerie_List, kaffee_List, suessikeiten_List, snacks_List, teig_Trocken_List, tiernahrung_List, getraenke_List,
                                               tiefkuehl_List, fleisch_List, milch_list, obst_List, sonstiges_List};
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
                categorieGrids.Add(gridStricked);
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
                categorieLabels.Add(lab_Categorie);
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
                        if (isAllListEmpty == true)
                        {
                            si.ListCheckBox.IsVisible = false;
                        }
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
                heigth = 400;
            }
            else
            if (count > 0)
            {
                if (count <= 4)
                {
                    heigth = 150;
                }
                if (count > 4 && count <= 10)
                {
                    heigth = 250;
                }
                if (count > 10)
                {
                    heigth = 350;
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
                    case "Tiefkühlwaren": return tiefkuehl_List;
                    case "Obst & Gemüse": return obst_List;
                    case "Süßigkeiten": return suessikeiten_List;
                    case "": return defaultList;
                    case "Leere Liste": return allEmptyList;
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
                HeightRequest = 200.0,
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
        private async void RemoveItem(ShopItems itemData, string categorie)
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
                    case "Tee/Kaffee/Brot": kaffee_List.Remove(itemData); break;
                    case "Süßigkeiten": suessikeiten_List.Remove(itemData); break;
                    case "": defaultList.Remove(itemData); break;
                    default: sonstiges_List.Remove(itemData); break;
                }

                itemData.ListCheckBox = new CheckBox();
                itemData.ListCheckBox.IsChecked = true;
                itemData.ListCBIsChecked = true;
                itemData.ListCBColor = Color.YellowGreen;
                itemData.Decorations = TextDecorations.Strikethrough;
                strikedItemList.Add(itemData);
                await DeleteFromDB(itemData.Id);

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
                case "Drogerie": AddtoList(categorie, listTag, db, drogerie_List); break;
                case "Milchprodukte": AddtoList(categorie, listTag, db, milch_list); break;
                case "Snacks": AddtoList(categorie, listTag, db, snacks_List); break;
                case "Tiernahrung": AddtoList(categorie, listTag, db, tiernahrung_List); break;
                case "Tee/Kaffee/Brot": AddtoList(categorie, listTag, db, kaffee_List); break;
                case "Getränke": AddtoList(categorie, listTag, db, getraenke_List); break;
                case "Teig-/Trockenwaren": AddtoList(categorie, listTag, db, teig_Trocken_List); break;
                case "Fleisch/Wurst/Fisch": AddtoList(categorie, listTag, db, fleisch_List); break;
                case "Tiefkühlwaren": AddtoList(categorie, listTag, db, tiefkuehl_List); break;
                case "Obst & Gemüse": AddtoList(categorie, listTag, db, obst_List); break;
                case "Süßigkeiten": AddtoList(categorie, listTag, db, suessikeiten_List); break;
                case "": AddtoDefaultList(listTag, db); break;
                default: AddtoList("Sonstiges", listTag, db, sonstiges_List); break;
            }

        }
       
        private async Task DeleteFromDB(int id)
        {
            await App.Db.DeleteItemAsync(id);
        }
        #endregion Methods
        #region Events
        private async void ListViewStrikedItem_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           // mainPageViewModel.StrikedItemsToDO(sender, listViewStrikedItem, myShop, savedLists);
            var itemData = (sender as ListView).SelectedItem as ShopItems;
           
            if (itemData.ListCBIsChecked == false)
            {
                itemData.ListCBIsChecked = true;
            }
            else
            {
                itemData.ListCBIsChecked = false;
                strikedItemList.Remove(itemData);
                string typ = itemData.Typ;
                if (myShop.Name == "Keinen")
                {
                    typ = "";
                }

                if (!(GetItemSource(typ).Contains(itemData)))
                {
                    bool hit = false;
                    itemData.ListCheckBox = new CheckBox();
                    itemData.ListCheckBox.IsChecked = false;
                    var db = App.Db;
                    for (int i = 0; i < db.GetAllItemsAsync().Result.Count; i++)
                    {
                        if (db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            hit = true;
                        }
                    }
                    if (hit == false)
                    {
                      await mainPageViewModel.AddItemToDB(itemData.Tag, itemData.ListTag, itemData.ItemName, itemData.Typ);
                    }

                    for (int i = 0; i < db.GetAllItemsAsync().Result.Count; i++)
                    { // ein tag hinzufügen
                        if (db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            ShopItems si = db.GetAllItemsAsync().Result[i];
                            if (!(GetItemSource(typ).Contains(si)))
                                GetItemSource(typ).Add(si);
                        }
                    }
                }

            }
            listViewStrikedItem.SelectedItem = null;
            await SetCategories(itemData);
            mainPageViewModel.SetInfoToDoDone();
        }

        private void ListItemView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           
            var itemData = (sender as ListView).SelectedItem as ShopItems;
            if (isAllListEmpty)
            {
                (sender as ListView).SelectedItem = null;
            }
            else
            {
                string typ = itemData.Typ;
                if (myShop.Name == "Keinen")
                {
                    typ = "";
                }
                switch (typ)
                {
                    case "Drogerie": RemoveItem(itemData, typ); break;
                    case "Milchprodukte": RemoveItem(itemData, typ); break;
                    case "Snacks": RemoveItem(itemData, typ); break;
                    case "Tiernahrung": RemoveItem(itemData, typ); break;
                    case "Tee/Kaffee/Brot": RemoveItem(itemData, typ); break;
                    case "Getränke": RemoveItem(itemData, typ); break;
                    case "Teig-/Trockenwaren": RemoveItem(itemData, typ); break;
                    case "Fleisch/Wurst/Fisch": RemoveItem(itemData, typ); break;
                    case "Tiefkühlwaren": RemoveItem(itemData, typ); break;
                    case "Obst & Gemüse": RemoveItem(itemData, typ); break;
                    case "Süßigkeiten": RemoveItem(itemData, typ); break;
                    case "": RemoveItem(itemData, typ); break;
                    default: RemoveItem(itemData, "Sonstiges"); break;
                }

            (sender as ListView).SelectedItem = null;
                (sender as ListView).ItemsSource = null;
                if (myShop.Name == "Keinen")
                {
                    (sender as ListView).ItemsSource = GetItemSource("");
                }
                else
                {
                    (sender as ListView).ItemsSource = GetItemSource(typ);
                }

                // Wenn eine Liste leer ist soll die entsprechende Categorie + ListView aus dem grid entfernt werden
                // zum identifizieren der Labels und der Grids wurden sie in einer generischen Liste gespeichert
                if (GetItemSource(typ).Count == 0 && !(typ == ""))
                {
                    // categorieGrid.Children.Remove();
                    foreach (var item in categorieLabels)
                    {
                        if (item.Text == typ)
                        {
                            foreach (var grid in categorieGrids)
                            {
                                if (grid.Children.Contains(item))
                                {
                                    grid.Children.Remove(item);
                                    grid.Children.Remove((sender as ListView));
                                }
                            }

                        }
                    }
                }
                listViewStrikedItem.ItemsSource = null;
                listViewStrikedItem.ItemsSource = strikedItemList;

                mainPageViewModel.SetInfoToDoDone();
                // ListItemView(itemData.ListTag, itemData);
            }





        }
        #endregion Events

        #region Add to list
        private void AddtoList(string categorie, string listTag, Database db, List<ShopItems> list)
        {
            list.Clear();

            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        if (db.GetAllItemsAsync().Result[i].Typ == categorie)
                        {
                            list.Add(db.GetAllItemsAsync().Result[i]);
                        }

                    }
                }

            }
           
        }
       
      
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
        private bool IsAllListEmpty()
        {
            bool empty = true;
            foreach (var item in allLists)
            {
                if (item.Count > 0)
                {
                    empty = false;
                    break;
                }
            }
            return empty;
        }
        #endregion Add to List
    }
}
