using Shoppinglist.Model;
using Shoppinglist.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Shoppinglist.ViewModels
{
    public class MainPageViewModel : BaseVM
    {
        #region Fields
        Grid mainGrid,grid;
        Label lab_newList, lab_newListColor;
        Entry entryNewList, entryNewItem;
        CheckBox checkBoxColor1, checkBoxColor2, checkBoxColor3, checkBoxColor4;
        Color backgroundColor, foregroundColor;
        Button btn_OK, btn_Ready;
        ListView listView, listNamesView, listItemView, listViewStrikedItem;
        MainPage mainPage;
        TextDecorations decorations;
        ShopItems shopitem;
        public List<ShopItems> createList, shopListItems, strikedItemList;
        public List<SavedLists> shopListNames;
        string listColor;
        string listName;
        
        #endregion Fields
        #region Property Change 
        public TextDecorations Decorations 
        { 
            get => decorations;
            set => SetProperty(ref decorations, value);
        }
        public List<ShopItems> Slist
        {
            get => createList;
            set => SetProperty(ref createList, value);
        }
        #endregion Property Change
        public INavigation Navigation { get; set; }
        public MainPageViewModel(INavigation navigation, Grid grid, Grid mainGrid, MainPage mainPage)
        {
            this.Navigation = navigation; 
            this.grid = grid;
            this.mainGrid = mainGrid;
            this.mainPage = mainPage;
            listColor = "";
            backgroundColor = Color.White;
            foregroundColor = Color.Black;
            grid.BackgroundColor = backgroundColor;
            shopitem = new ShopItems();
            createList = new List<ShopItems>();
            shopListItems = new List<ShopItems>();
            shopListNames = new List<SavedLists>();
            strikedItemList = new List<ShopItems>();
            MainMenue();
        }
        #region Create new List
        #region Methods Create List
        private void NewShoppingList()
        {
            //Name liste, farbe liste, 
            grid.Children.Clear();
            lab_newList =  CreateLabel("Den Namen der Liste eingeben",LayoutOptions.StartAndExpand, LayoutOptions.CenterAndExpand, 0);
            entryNewList = CreateEntry("Name der Liste", LayoutOptions.StartAndExpand, LayoutOptions.CenterAndExpand, 1);
            entryNewList.TextChanged += EntryNewList_TextChanged;
            lab_newListColor = CreateLabel("Die Farbe der Liste wählen", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, 2);
            checkBoxColor1 = CreateCheckBox(LayoutOptions.CenterAndExpand,3);
            checkBoxColor2 = CreateCheckBox(LayoutOptions.CenterAndExpand,4);
            checkBoxColor3 = CreateCheckBox(LayoutOptions.CenterAndExpand,5);
            checkBoxColor4 = CreateCheckBox(LayoutOptions.CenterAndExpand,6);
            CreateColorLabel(Color.FromHex("#336b87"), "Stone", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand,3);
            CreateColorLabel(Color.FromHex("#598234"), "Meadow", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, 4);
            CreateColorLabel(Color.FromHex("#07575B"), "Ocean", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, 5);
            CreateColorLabel(Color.FromHex("#2A3132"), "Shadow", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, 6);
            btn_OK = new Button
            {
                Text = "OK",
                BorderColor = Color.Black,
                BorderWidth = 4,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
                FontSize = 30.0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            checkBoxColor1.IsChecked = true;
            btn_OK.IsEnabled = false;
            btn_OK.Clicked += Btn_OK_Clicked;
            Grid.SetRow(btn_OK, 7);
            grid.Children.Add(btn_OK);
           
        }
        private void CreateToolBarItem()
        {
            ToolbarItem item = new ToolbarItem
            {
                Text = "Example Item",
                IconImageSource = ImageSource.FromFile("hauptmenue24.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            item.Clicked += ToolBarItem_Clicked;
            mainPage.ToolbarItems.Add(item);
        }
        private void CreateColorLabel(Color color, string txt, LayoutOptions vertOptions, LayoutOptions horOptions, short row)
        {
            Label label = new Label
            {
                BackgroundColor = color,
                Text = txt,
                TextColor = Color.White,
                FontSize = 16.0,
                WidthRequest = 200,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = horOptions,
                VerticalOptions = vertOptions
            };
            Grid.SetRow(label, row);
            grid.Children.Add(label);
        }
        private Label CreateLabel(string txt, LayoutOptions vertOptions, LayoutOptions horOptions, short row)
        {
            Label lab = new Label
            {
                Text = txt,
                FontSize = 16.0,
                TextColor = foregroundColor,
                HorizontalOptions = horOptions,
                VerticalOptions = vertOptions
            };
            Grid.SetRow(lab, row);
            grid.Children.Add(lab);
            return lab;
        }
        private CheckBox CreateCheckBox(LayoutOptions vertOptions, short row)
        {
            CheckBox checkBox = new CheckBox
            {
                Color = Color.Green,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = vertOptions
            };
            Grid.SetRow(checkBox, row);
            grid.Children.Add(checkBox);
            checkBox.CheckedChanged += CheckBox_CheckedChanged;
            
            return checkBox;
        }
        private Entry CreateEntry(string txt, LayoutOptions vertOptions, LayoutOptions horOptions, short row)
        {
            Entry entry = new Entry
            {
                Placeholder = txt,
                PlaceholderColor = Color.Gray,
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                MaxLength = 25,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                FontSize = 16.0,
                WidthRequest = 200,
                HorizontalOptions = horOptions,
                VerticalOptions = vertOptions
            };
            Grid.SetRow(entry, row);
            grid.Children.Add(entry);
            return entry;
        }
        private void CreateListView(short row, ShopItems shopitem)
        {
            
            if (this.listView != null)
            {
                grid.Children.Remove(this.listView);
            }
           
            ListView listView = new ListView
            {
               
                ItemsSource = createList,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ItemName");
                    lab.FontSize = 16.0;
                    lab.TextColor = foregroundColor;
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
                                                lab
                                            }
                                        }
                            }
                        }
                    };
                })
            };
            this.listView = listView;
            this.listView.ItemTapped += ListView_ItemTapped;
            Grid.SetRow(listView, row);
            grid.Children.Add(listView);
            //this.listView.ItemSelected += ListView_ItemSelected;
        }
        private void CheckIsChecked()
        {
            if (checkBoxColor1.IsChecked)
            {
                checkBoxColor1.IsChecked = true;
                checkBoxColor2.IsChecked = false;
                checkBoxColor3.IsChecked = false;
                checkBoxColor4.IsChecked = false;
            }
            if (checkBoxColor2.IsChecked)
            {
                checkBoxColor1.IsChecked = false;
                checkBoxColor2.IsChecked = true;
                checkBoxColor3.IsChecked = false;
                checkBoxColor4.IsChecked = false;
            }
            if (checkBoxColor3.IsChecked)
            {
                checkBoxColor1.IsChecked = false;
                checkBoxColor2.IsChecked = false;
                checkBoxColor3.IsChecked = true;
                checkBoxColor4.IsChecked = false;
            }
            if (checkBoxColor4.IsChecked)
            {
                checkBoxColor1.IsChecked = false;
                checkBoxColor2.IsChecked = false;
                checkBoxColor3.IsChecked = false;
                checkBoxColor4.IsChecked = true;
            }
        }
        private void CheckBoxesColor()
        {
            if (entryNewList.Text != "")
            {
                grid.Children.Clear();

                if (checkBoxColor1.IsChecked)
                {
                    listColor = "#336b87";
                    mainPage.BackgroundColor = Color.FromHex("#336b87");
                    grid.BackgroundColor = Color.FromHex("#336b87");
                    mainGrid.BackgroundColor = Color.FromHex("#336b87");
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#336b87");
                }
                if (checkBoxColor2.IsChecked)
                {
                    listColor = "#598234";
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#598234");
                    grid.BackgroundColor = Color.FromHex("#598234");
                    mainGrid.BackgroundColor = Color.FromHex("#598234");
                    mainPage.BackgroundColor = Color.FromHex("#598234");

                }
                if (checkBoxColor3.IsChecked)
                {
                    listColor = "#07575B";
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#07575B");
                    grid.BackgroundColor = Color.FromHex("#07575B");
                    mainGrid.BackgroundColor = Color.FromHex("#07575B");
                    mainPage.BackgroundColor = Color.FromHex("#07575B");

                }
                if (checkBoxColor4.IsChecked)
                {
                    listColor = "#2A3132";
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#2A3132");
                    grid.BackgroundColor = Color.FromHex("#2A3132");
                    mainGrid.BackgroundColor = Color.FromHex("#2A3132");
                    mainPage.BackgroundColor = Color.FromHex("#2A3132");

                }
                listName = entryNewList.Text;
                mainPage.Title = listName;
            }
        }
        private void BackToMain()
        {
            strikedItemList.Clear();    
            shopListItems.Clear();
            shopListNames.Clear();
            createList.Clear();
            mainPage.BackgroundColor = Color.White;
            grid.BackgroundColor = Color.White;
            mainGrid.BackgroundColor = Color.White;
            backgroundColor = Color.White;
            foregroundColor = Color.Black;
            mainPage.ToolbarItems.Clear();
            grid.Children.Clear();
            if (btn_Ready != null)
            btn_Ready.IsVisible = false;
            if (entryNewItem != null)
            entryNewItem.IsVisible = false;
            MainMenue();
        }
        #endregion Methods Create List
        #region Events CreateList
        private void ToolBarItem_Clicked(object sender, EventArgs e)
        {
            BackToMain();
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckIsChecked();
        }
        private void EntryNewList_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryNewList.Text != "")
            {
                btn_OK.IsEnabled = true;
            }
            else
            {
                btn_OK.IsEnabled = false;
            }
        }
        private void Btn_OK_Clicked(object sender, EventArgs e)
        {
            mainPage.Title = "Neue Liste Erstellen";
            CreateToolBarItem();
            CheckBoxesColor();
            btn_Ready = new Button
            {
                Text = "Ready",
                BorderColor = Color.Black,
                BorderWidth = 4,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
                FontSize = 16.0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                IsEnabled = false
                
            };
            btn_Ready.Clicked += Btn_Ready_Clicked;
            entryNewItem = CreateEntry("Artikel hinzufügen", LayoutOptions.StartAndExpand, LayoutOptions.FillAndExpand, 0);
            entryNewItem.Completed += EntryNewItem_Completed;
            mainGrid.Children.Add(btn_Ready);
        }

        private void Btn_Ready_Clicked(object sender, EventArgs e)
        {
           
            //Liste übernehmen und in Datenbank Speichern
            foreach (var item in createList)
            {
               
                AddItemToDB(item.Tag, item.ListName, item.ItemName);
               
            }
            AddListToDB(mainPage.Title, listColor);
            WriteToast.ShowLongToast("Liste wurde gespeichert");
            BackToMain();
        }
       
        private void EntryNewItem_Completed(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string tag = entryNewItem.Text + date.Hour + date.Minute + date.Second + date.Millisecond;
            string txt = entryNewItem.Text;
            IsListEmpty();
            shopitem = new ShopItems();
            shopitem.ItemName = txt;
            shopitem.ListName = listName;
            shopitem.Tag = tag;
            
            createList.Add(shopitem);
            CreateListView(1, shopitem);

            entryNewItem.Text = "";
            entryNewItem.Focus();
        }

        private void IsListEmpty()
        {
            if (createList.Count == 0)
            {
                btn_Ready.IsEnabled = false;
            }
            else
                btn_Ready.IsEnabled = true;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as ShopItems;
          
           
                createList.Remove(itemData);
                IsListEmpty();
                listView.SelectedItem = null;
                grid.Children.Remove(listView);
                CreateListView(1, itemData);
            
        }
        #endregion Events CreateList

        #endregion Create new List
        #region Main Menue
        #region Methods Main Menue
        private void MainMenue()
        {
            // auswahl neue liste erstellen,
            // bestand bereits angelegter listen einsehen und auswählen, 
            mainPage.Title = "Hauptmenue";
            
            CreateMainmenueBTN("Neue Liste erstellen", "neueListe48.png", 0);
            CreateMainmenueBTN("Liste Wählen", "einkauf48.png" ,1);
           
        }

        private void CreateMainmenueBTN(string txt,string icon, short row)
        {
            Button btn = new Button
            {
                Text = txt,
                ImageSource = icon,
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 0),
                BorderColor = Color.Black,
                BorderWidth = 4,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
                FontSize = 20.0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            btn.Clicked += Btn_Clicked;
            Grid.SetRow(btn, row);
            grid.Children.Add(btn);
        }
        private void LoadShoppingList()
        {
            grid.Children.Clear();
            mainPage.Title = "Liste Wählen";
            GetListNames();
           
        }
        private void ListNamesView()
        {
            listNamesView = new ListView
            {
                ItemsSource = shopListNames,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ListName");
                   
                    lab.FontSize = 16.0;
                    lab.TextColor = foregroundColor;
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
                                                lab
                                            }
                                        }
                            }
                        }
                    };
                })
            };
            listNamesView.ItemTapped += ListView_ItemTapped1; ;
            Grid.SetRow(listNamesView, 1);
            grid.Children.Add(listNamesView);
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
                    lab.FontSize = 16.0;
                    lab.TextColor = foregroundColor;
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
            listViewStrikedItem.ItemTapped += ListViewStrikedItem_ItemTapped;
            gridStricked.Children.Add(listViewStrikedItem);
            Grid.SetRow(gridStricked, 1);
            grid.Children.Add(gridStricked);
        }

       

        private void ListItemView(string title,  ShopItems si)
        {
            grid.Children.Clear();
            mainPage.Title = title;
          
            Grid gridStricked = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            listItemView = new ListView
            {
                ItemsSource = shopListItems,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Always,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 300.0,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ItemName");
                    lab.SetBinding(Label.TextDecorationsProperty, "Decorations");
                    lab.FontSize = 16.0;
                    lab.TextColor = foregroundColor;
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
            listItemView.ItemTapped += ListItemView_ItemTapped;
            gridStricked.Children.Add(listItemView);
            Grid.SetRow(gridStricked, 0);
            grid.Children.Add(gridStricked);
        }
    
    
        #endregion Methods Main Menue
        #region Events mainMenue
        //Event bei auswahl der Liste
        private void ListView_ItemTapped1(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as SavedLists;
            mainPage.BackgroundColor = Color.FromHex(itemData.ListColor);
            grid.BackgroundColor = Color.FromHex(itemData.ListColor);
            mainGrid.BackgroundColor = Color.FromHex(itemData.ListColor);
            foregroundColor = Color.White;
            LoadListFromDB(itemData.ListName);
            ListItemView(itemData.ListName, shopitem);
            listNamesView.SelectedItem = null;
           
        }
        //Event liste oben
        private void ListItemView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as ShopItems;
           
            if (itemData.ListCBIsChecked == false)
            {
                itemData.ListCBIsChecked = true;
                shopListItems.Remove(itemData);
                itemData.ListCheckBox = new CheckBox();
                itemData.ListCheckBox.IsChecked = true;
                strikedItemList.Add(itemData);

                
                DeleteFromDB(itemData.Id);
                
              
            }
            else
            {
                itemData.ListCBIsChecked = false;
                //AddItemToDB(itemData.ListName, itemData.ItemName);
            }
            listItemView.SelectedItem = null;

            ListItemView(itemData.ListName, itemData);
            ListStrikedItems(itemData);


        }
        //Event liste unten
        private void ListViewStrikedItem_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as ShopItems;
            if (itemData.ListCBIsChecked == false)
            {
                itemData.ListCBIsChecked = true;

            }
            else
            {
                itemData.ListCBIsChecked = false;
                strikedItemList.Remove(itemData);
                if (!(shopListItems.Contains(itemData)))
                {
                    bool hit = false;
                    itemData.ListCheckBox = new CheckBox();
                    itemData.ListCheckBox.IsChecked = false;
                    for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
                    {
                        if (App.Db.GetAllItemsAsync().Result[i] == itemData)
                        {
                            hit = true;
                        }
                    }
                    if (hit == false)
                    {
                        AddItemToDB(itemData.Tag,itemData.ListName, itemData.ItemName);
                    }
                    
                    for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
                    { // ein tag hinzufügen
                        if (App.Db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            ShopItems si = App.Db.GetAllItemsAsync().Result[i];
                            if (!(shopListItems.Contains(si)))
                            shopListItems.Add(si);
                        }
                    }
                }

            }
            listItemView.SelectedItem = null;
            ListItemView(itemData.ListName, itemData);
            ListStrikedItems(itemData);
        }

        private static void SelectDb()
        {
            for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
            {
                int id = App.Db.GetAllItemsAsync().Result[i].Id;
                ShopItems art = App.Db.GetItemAsync(id).Result;
                Debug.WriteLine(art.ItemName + " " + art.Id);
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            string options = (sender as Button).Text;
            switch (options)
            {
                case "Neue Liste erstellen": NewShoppingList(); break;
                case "Liste Wählen": LoadShoppingList(); CreateToolBarItem(); break;
            }
        }
        #endregion Events mainMenue
        #endregion Main Menue
        #region Database
        private async void AddItemToDB(string tag,string listName, string itemName)
        {
            await App.Db.AddToDBAsync(new Models.ShopItems
            {
                Tag = tag,
                ListName = listName,
                ItemName = itemName,
            });
            
        }
        private async void AddListToDB(string listName, string listColor)
        {
            await App.DbLists.AddToDBAsync(new Models.SavedLists
            {
               
                ListName = listName,
                ListColor = listColor
            });
        }
        private async void DeleteFromDB(int id)
        {
            await App.Db.DeleteItemAsync(id);
        }
        private async void DeleteListFromDB(string listName)
        {
            if (App.Db.GetAllItemsAsync().Result.Count > 0)
            {

                int id = 0;
                for (int i = 0; i < App.Db.GetDBCount().Result; i++)
                {
                    if (App.Db.GetAllItemsAsync().Result[i].ListName == listName)
                    {
                        id = App.Db.GetAllItemsAsync().Result[i].Id;
                        DeleteFromDB(id);
                    }
                }

            }
            if (App.DbLists.GetAllItemsAsync ().Result.Count > 0)
            {
                int id = 0;
                for (int i = 0; i < App.DbLists.GetDBCount().Result; i++)
                {
                    if (App.DbLists.GetAllItemsAsync().Result[i].ListName == listName)
                    {
                        id = App.DbLists.GetAllItemsAsync().Result[i].Id;
                        await App.DbLists.DeleteItemAsync(id);
                    }
                }
            }
        }
        //erst wenn die liste ausgewählt wurde
        private void LoadListFromDB(string listName)
        {
            //hier darf immer nur die liste enthalten sein die aktuell gewählt wurde
            shopListItems.Clear();
            if (App.Db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < App.Db.GetDBCount().Result; i++)
                {
                    if (App.Db.GetAllItemsAsync().Result[i].ListName == listName)
                    {
                        shopListItems.Add(App.Db.GetAllItemsAsync().Result[i]);
                    }
                }

            }
        }
        private void GetListNames()
        {
            if (App.DbLists.GetAllItemsAsync().Result.Count > 0)
            {
                string getName = string.Empty;
                for (int i = 0; i < App.DbLists.GetDBCount().Result; i++)
                {
                   shopListNames.Add(App.DbLists.GetAllItemsAsync().Result[i]);
                }
                ListNamesView();
            }
            else
            {
                CreateLabel("Noch keine Liste erstellt", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, 3);
            }
        }
        #endregion Database
    }
}
