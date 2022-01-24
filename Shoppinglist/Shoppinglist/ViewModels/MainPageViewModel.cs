﻿using Shoppinglist.Model;
using Shoppinglist.Models;
using Shoppinglist.Views;
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
        Frame frameNamelist;
        Label lab_newList, lab_newListColor;
        Entry entryNewList, entryNewItem;
        CheckBox[] checkBoxColor;
        Color backgroundColor, foregroundColor;
        Button btn_OK, btn_Ready;
        ListView listView, listNamesView, listMyShops;
        MainPage mainPage;
        TextDecorations decorations;
        ShopItems shopitem;
        SavedLists savedLists;
        public List<ShopItems> createList, shopListItems, strikedItemList;
        public List<SavedLists> shopListNames;
        public List<MyShop> myShopNames;
        string listColor;
        string listNameF , listTagF;
        string[] listColorHex;
        string[] listColorNames;
        
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
        public INavigation Navigation { get; set; }
        #endregion Property Change

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
            myShopNames = new List<MyShop>();
            checkBoxColor = new CheckBox[4];
            listColorHex = new string[] { "#336b87", "#598234", "#07575B", "#2A3132" };
            listColorNames = new string[] { "Stone", "Meadow", "Ocean", "Shadow" };
            MainMenue();
        }
        
        #region Methods 
        private void NewShoppingList()
        {
            //Name liste, farbe liste, 
            mainPage.Title = "Neue Liste erstellen";
            grid.Children.Clear();
            CreateToolBarItem();
            frameNamelist = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 1);
            ImageButton imgNameList = CreateImageButton(0, "hinweis48.png");
            lab_newList = CreateLabel("Den Namen der Liste eingeben", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, "NanumPenScript-Regular", 0);
            entryNewList = CreateEntry("Name der Liste", LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, Color.FromHex("#86AC41"), Color.Gray, Color.White);
            frameNamelist.Content = entryNewList;
            entryNewList.TextChanged += EntryNewList_TextChanged;
            ImageButton imgChooseColor = CreateImageButton(2, "hinweis48.png");
            lab_newListColor = CreateLabel("Die Farbe der Liste wählen", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, "NanumPenScript-Regular", 2);
          
            for (int i = 0; i < checkBoxColor.Length; i++)
            {
                checkBoxColor[i] = CreateCheckBox(LayoutOptions.CenterAndExpand, (short)(3 + i));
                CreateColorLabel(Color.FromHex(listColorHex[i]), listColorNames[i], LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, (short)(3 + i));
            }
            btn_OK = new Button
            {
                Text = "OK",
                BorderColor = Color.Black,
                BorderWidth = 4,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
                FontSize = 25.0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };
            checkBoxColor[0].IsChecked = true;
            btn_OK.IsEnabled = false;
            btn_OK.Clicked += Btn_OK_Clicked;
            Grid.SetRow(btn_OK, 7);
            grid.Children.Add(btn_OK);
        }

        private ImageButton CreateImageButton(short row, string imgPath)
        {
            ImageButton imageButton = new ImageButton
            {
                Source = imgPath,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                IsEnabled = false
            };
            Grid.SetRow(imageButton, row);
            grid.Children.Add(imageButton);
            return imageButton;
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
        private Frame CreateFrame(Color color, LayoutOptions vertOptions, LayoutOptions horOptions, short row)
        {
            Frame frame = new Frame
            {
                BackgroundColor = color,
                VerticalOptions =vertOptions,
                HorizontalOptions = horOptions,
                CornerRadius = 20,
                HasShadow = true,
                BorderColor = Color.Black
                
            };
            Grid.SetRow(frame, row);
            grid.Children.Add(frame);
            return frame;
        }
        private void CreateColorLabel(Color color, string txt, LayoutOptions vertOptions, LayoutOptions horOptions, short row)
        {
            Label label = new Label
            {
                BackgroundColor = color,
                Text = txt,
                TextColor = Color.White,
                FontSize = 25.0,
                WidthRequest = 200,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = horOptions,
                VerticalOptions = vertOptions
            };
            Grid.SetRow(label, row);
            grid.Children.Add(label);
        }
        private Label CreateLabel(string txt, LayoutOptions vertOptions, LayoutOptions horOptions, string fontFam, short row)
        {
            Label lab = new Label
            {
                Text = txt,
                FontSize = 20.0,
                FontAttributes = FontAttributes.Bold,
                FontFamily = fontFam,
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
                VerticalOptions = vertOptions,
                
            };
            Grid.SetRow(checkBox, row);
            grid.Children.Add(checkBox);
           
            checkBox.CheckedChanged += CheckBox_CheckedChanged;
            return checkBox;
        }

       

        

        public Entry CreateEntry(string txt, LayoutOptions vertOptions, LayoutOptions horOptions, Color background, Color placeholder, Color text)
        {
            Entry entry = new Entry
            {
                Placeholder = txt,
                PlaceholderColor = placeholder,
                BackgroundColor = background,
                TextColor = text,
                MaxLength = 40,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                FontSize = 16.0,
                WidthRequest = 200,
                HorizontalOptions = horOptions,
                VerticalOptions = vertOptions
            };
          
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
            /* int checkedIndex = -1;
             for (int i = 0; i < checkBoxColor.Length; i++)
             {
                 if (checkBoxColor[i].IsChecked)
                 {
                     checkedIndex = i;
                 }
             }
             for (int i = 0; i < checkBoxColor.Length; i++)
             {
                 if (checkedIndex != i)
                 {
                     checkBoxColor[i].IsChecked = false;
                 }
             }*/
           
            for (int i = 0; i < checkBoxColor.Length; i++)
            {
                if (checkBoxColor[i].IsChecked)
                {
                    checkBoxColor[i].IsChecked = true;
                   
                    switch (i)
                    {
                        case 0:
                            checkBoxColor[1].IsChecked = false;
                            checkBoxColor[2].IsChecked = false;
                            checkBoxColor[3].IsChecked = false;
                            break;
                        case 1:
                            checkBoxColor[0].IsChecked = false;
                            checkBoxColor[2].IsChecked = false;
                            checkBoxColor[3].IsChecked = false;
                            break;
                        case 2:
                            checkBoxColor[0].IsChecked = false;
                            checkBoxColor[1].IsChecked = false;
                            checkBoxColor[3].IsChecked = false;
                            break;
                        case 3:
                            checkBoxColor[0].IsChecked = false;
                            checkBoxColor[1].IsChecked = false;
                            checkBoxColor[2].IsChecked = false;
                            break;
                    }
                }
                
            }
          

        }
        private void CheckBoxesColor()
        {
            if (entryNewList.Text != "")
            {
                grid.Children.Clear();
                for (int i = 0; i < checkBoxColor.Length; i++)
                {
                    if (checkBoxColor[i].IsChecked)
                    {
                        listColor = listColorHex[i];
                        mainPage.BackgroundColor = Color.FromHex(listColor);
                        grid.BackgroundColor = Color.FromHex(listColor);
                        mainGrid.BackgroundColor = Color.FromHex(listColor);
                        foregroundColor = Color.White;
                        backgroundColor = Color.FromHex(listColor);
                    }

                }
                    listNameF = entryNewList.Text;
                mainPage.Title = listNameF;
            }
        }
        private void BackToMain()
        {
            strikedItemList.Clear();    
            shopListItems.Clear();
            shopListNames.Clear();
            createList.Clear();
            myShopNames.Clear();
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
        private void MainMenue()
        {
            // auswahl neue liste erstellen,
            // bestand bereits angelegter listen einsehen und auswählen, Listen bearbeiten und Löschen
            mainPage.Title = "Hauptmenue";
            SetMainImg();
            Frame frame1 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 1);
            Frame frame2 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 2);
            Frame frame3 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 3);
            Frame frame4 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 4);
            Button btn1 = CreateMainmenueBTN("Neue Liste erstellen", "neueListe48.png");
            Button btn2 = CreateMainmenueBTN("Liste Wählen", "einkauf48.png");
            Button btn3 = CreateMainmenueBTN("Listen Verwalten", "edit48.png");
            Button btn4 = CreateMainmenueBTN("Build your Shop", "einkauf48.png");
            frame1.Content = btn1;
            frame2.Content = btn2;
            frame3.Content = btn3;
            frame4.Content = btn4;

            //Liste bearbeiten fehlt
            //Liste Löschen fehlt
        }
        private void SetMainImg()
        {
            Image img = new Image
            {
                Source = "shopList.png",
                Aspect = Aspect.AspectFill
            };
            Grid.SetRow(img, 0);
            grid.Children.Add(img);
        }
        private Button CreateMainmenueBTN(string txt, string icon)
        {
            Button btn = new Button
            {
                Text = txt,
                ImageSource = icon,
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 0),
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#86AC41"),
                FontSize = 20.0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            btn.Clicked += Btn_Clicked;
            return btn;
        }
        private void LoadShoppingList()
        {
            grid.Children.Clear();
            mainPage.Title = "Liste Wählen";
            GetListNames(false, false);
        }

        private void SetInfoToDoDone()
        {
            Label lab_InfoToDo = new Label
            {
                Text = "Noch zu erledigen",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 25,
                TextColor = Color.FromHex("#C9FF00")
            };
            Label lab_InfoDone = new Label
            {
                Text = "Erledigt",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 25,
                TextColor = Color.FromHex("#C9FF00")
            };
            Grid.SetRow(lab_InfoToDo, 0);
            Grid.SetRow(lab_InfoDone, 2);
            grid.Children.Add(lab_InfoToDo);
            grid.Children.Add(lab_InfoDone);
        }

        private void ListNamesView(bool edit, bool delete)
        {
            listNamesView = new ListView
            {
                ItemsSource = shopListNames,
                ItemTemplate = new DataTemplate(() =>
                {
                    Image img = new Image();
                    img.Source = "this24.png";
                    img.VerticalOptions = LayoutOptions.CenterAndExpand;
                    img.HorizontalOptions = LayoutOptions.StartAndExpand;

                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ListName");

                    lab.FontSize = 20.0;
                    lab.TextColor = foregroundColor;
                    lab.FontAttributes = FontAttributes.Bold;
                    lab.FontFamily = "AD";
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
                                                img,
                                                lab
                                            }
                                        }
                            }
                        }
                    };
                })
            };
            if (!(edit) && !(delete))
            {
                listNamesView.ItemTapped += ListNamesView_ItemTapped;
            }
            else
            {
                if (edit)
                {
                    listNamesView.ItemTapped += ListNamesView_ItemTapped1; ;
                }
                if (delete)
                {
                    listNamesView.ItemTapped += ListNamesView_Delete_ItemTapped;
                }
            }

            Grid.SetRow(listNamesView, 1);
            grid.Children.Add(listNamesView);
        }

        private void ListNamesView_ItemTapped1(object sender, ItemTappedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyShopList()
        {
            listMyShops = new ListView
            {
                ItemsSource = myShopNames,
                ItemTemplate = new DataTemplate(() =>
                {
                    Image img = new Image();
                    img.Source = "this24.png";
                    img.VerticalOptions = LayoutOptions.CenterAndExpand;
                    img.HorizontalOptions = LayoutOptions.StartAndExpand;

                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "Name");

                    lab.FontSize = 20.0;
                    lab.TextColor = foregroundColor;
                    lab.FontAttributes = FontAttributes.Bold;
                    lab.FontFamily = "AD";
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
                                                img,
                                                lab
                                            }
                                        }
                            }
                        }
                    };
                })
            };
            listMyShops.ItemTapped += ListMyShops_ItemTapped;

            Grid.SetRow(listMyShops, 1);
            grid.Children.Add(listMyShops);
        }

      

       
      
        private void Editlist()
        {
            mainPage.Title = "Listen Verwalten";
            grid.Children.Clear();
            Frame frame1 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 0);
            Button btn_Edit = new Button
            {
                Text = "Liste Bearbeiten",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White,
                FontSize = 20,
                BackgroundColor = Color.FromHex("#86AC41"),

            };
            btn_Edit.Clicked += Btn_Edit_Clicked1;
            frame1.Content = btn_Edit;
            Frame frame2 = CreateFrame(Color.FromHex("#86AC41"), LayoutOptions.CenterAndExpand, LayoutOptions.FillAndExpand, 1);
            Button btn_Delete = new Button
            {
                Text = "Liste Löschen",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White,
                FontSize = 20,
                BackgroundColor = Color.FromHex("#86AC41"),

            };
            btn_Delete.Clicked += Btn_Delete_Clicked;
            frame2.Content = btn_Delete;
        }
        private void RefreshDeleteList(SavedLists tag)
        {
            DeleteItemsFromList(tag.Tag);
            DeleteListFromDB(tag.Tag);
        }
        private void ShowDeleteList()
        {
            grid.Children.Clear();
            Label lab = CreateLabel("Liste zum Löschen wählen", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, string.Empty, 0);
            ImageButton img = CreateImageButton(0, "hinweis48.png");
            GetListNames(false, true);
        }
        private async void CallBuildYourShop()
        {
            BuildYourShopView bysView = new BuildYourShopView(this);
            await Navigation.PushAsync(bysView);
            Navigation.RemovePage(mainPage);
        }
        private void SelectShop(SavedLists itemData)
        {
            Label whichShopLabel = CreateLabel("Welchen Shop möchten sie nutzen?", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, "NanumPenScript-Regular", 0);
            savedLists = itemData;
            GetShopNames();
        }

       
        #endregion Methods 


        #region Events
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
            DateTime date = DateTime.Now;
            string tag = entryNewList.Text + date.Hour + date.Minute + date.Second + date.Millisecond;
            listTagF = tag;
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
            entryNewItem = CreateEntry("Artikel hinzufügen", LayoutOptions.StartAndExpand, LayoutOptions.FillAndExpand, Color.White, Color.Gray, Color.Black);
            entryNewItem.Completed += EntryNewItem_Completed;
            Grid.SetRow(entryNewItem, 0);
           
            grid.Children.Add(entryNewItem);
            mainGrid.Children.Add(btn_Ready);
        }

        private void Btn_Ready_Clicked(object sender, EventArgs e)
        {
           
            //Liste übernehmen und in Datenbank Speichern
            foreach (var item in createList)
            {
               
                AddItemToDB(item.Tag, listTagF, item.ItemName);
               
            }
            
            AddListToDB(listTagF, mainPage.Title, listColor);
            WriteToast.ShowLongToast("Liste wurde gespeichert");
            BackToMain();
        }
       
        private void EntryNewItem_Completed(object sender, EventArgs e)
        {
            if (entryNewItem.Text == "")
            {
                entryNewItem.Focus();
            }
            else
            {
                DateTime date = DateTime.Now;
                string tag = entryNewItem.Text + date.Hour + date.Minute + date.Second + date.Millisecond;

                string txt = entryNewItem.Text;
                IsListEmpty();
                shopitem = new ShopItems();
                shopitem.ItemName = txt;
                shopitem.ListTag = listTagF;
                shopitem.Tag = tag;
                shopitem.Typ = GetCategorie(txt);
                createList.Add(shopitem);
                CreateListView(1, shopitem);
                entryNewItem.Focus();
                entryNewItem.Text = "";
            }
           
           
        }

        private string GetCategorie(string itemName)
        {
            string typ = string.Empty;
            //Alle Assets auslesen um den Typ zu definieren, über die Klasse ItemTyp
            typ = ItemTyp.GetCategorieTyp(itemName);
            return typ;
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
        private void ListNamesView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as SavedLists;
            mainPage.BackgroundColor = Color.FromHex(itemData.ListColor);
            grid.BackgroundColor = Color.FromHex(itemData.ListColor);
            mainGrid.BackgroundColor = Color.FromHex(itemData.ListColor);
            foregroundColor = Color.White;
            SelectShop(itemData);
            listNamesView.SelectedItem = null;

        }
       

        
      

        public void ItemTappedDone(object sender, ListView listItemView, MyShop myShop, SavedLists savedLists)
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

           // ListItemView(itemData.ListTag, itemData);
            Categories categories = new Categories(grid, myShop, mainPage, shopListItems,strikedItemList, savedLists, this, itemData);
            categories.SetCategories();
           
            SetInfoToDoDone();
        }



        //Event liste unten
        

        public void StrikedItemsToDO(object sender, ListView listItemView, MyShop myShop, SavedLists savedLists)
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
                        if (App.Db.GetAllItemsAsync().Result[i].Tag == itemData.Tag)
                        {
                            hit = true;
                        }
                    }
                    if (hit == false)
                    {
                        AddItemToDB(itemData.Tag, itemData.ListTag, itemData.ItemName);
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
            Categories categories = new Categories(grid, myShop, mainPage, shopListItems, strikedItemList, savedLists, this, itemData);
            categories.SetCategories();
           
            SetInfoToDoDone();
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            string options = (sender as Button).Text;
            switch (options)
            {
                case "Neue Liste erstellen": NewShoppingList(); break;
                case "Liste Wählen": LoadShoppingList(); CreateToolBarItem(); break;//zuerst fragen welcher Shop genutzt werden soll,dann die listen anzeigen
                case "Listen Verwalten": Editlist(); CreateToolBarItem(); break;
                case "Build your Shop": CallBuildYourShop(); break;
            }
        }
        private void Btn_Edit_Clicked1(object sender, EventArgs e)
        {
            grid.Children.Clear();
            Label lab = CreateLabel("Liste zum Bearbeiten wählen", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, string.Empty, 0);
            ImageButton img = CreateImageButton(0, "hinweis48.png");
            GetListNames(true, false);
        }
        private void Btn_Delete_Clicked(object sender, EventArgs e)
        {
            ShowDeleteList();
        }

      

       
        private void ListNamesView_Delete_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as SavedLists;
            shopListNames.Clear();
            RefreshDeleteList(itemData);
        }
        #endregion Events 





        #region Database
        private async void DeleteFromDB(int id)
        {
            await App.DbLists.DeleteItemAsync(id);
        }
        private async void AddItemToDB(string tag,string listName, string itemName)
        {
            await App.Db.AddToDBAsync(new Models.ShopItems
            {
                Tag = tag,
                ListTag = listName,
                ItemName = itemName,
               // Typ = typ
            });
            
        }
        private async void AddListToDB(string tag,string listName, string listColor)
        {
            await App.DbLists.AddToDBAsync(new Models.SavedLists
            {
                Tag=tag,
                ListName = listName,
                ListColor = listColor
            });
        }
       
        private async void DeleteListFromDB(string listTag)
        {
           
            // Die liste selbst löschen
            var dbLists = App.DbLists;
            if (dbLists.GetAllItemsAsync().Result.Count > 0)
            {
                int id = 0;
                for (int i = 0; i < dbLists.GetDBCount().Result; i++)
                {
                    if (dbLists.GetAllItemsAsync().Result[i].Tag == listTag)
                    {
                        id = dbLists.GetAllItemsAsync().Result[i].Id;
                        await dbLists.DeleteItemAsync(id);
                    }
                }
            }
            ShowDeleteList();
        }

        private async void DeleteItemsFromList(string listTag)
        {
            //Alle artikel aus der liste löschen
            var db = App.Db;
            if (db.GetAllItemsAsync().Result.Count > 0)
            {

                int id = 0;
                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        id = db.GetAllItemsAsync().Result[i].Id;
                        await db.DeleteItemAsync(id);
                    }
                }

            }
        }

        //erst wenn die liste ausgewählt wurde
        private void LoadListFromDB(string listTag)
        {
            //hier darf immer nur die liste enthalten sein die aktuell gewählt wurde
            shopListItems.Clear();
            var db = App.Db;
            if (db.GetAllItemsAsync().Result.Count > 0)
            {


                for (int i = 0; i < db.GetDBCount().Result; i++)
                {
                    if (db.GetAllItemsAsync().Result[i].ListTag == listTag)
                    {
                        shopListItems.Add(db.GetAllItemsAsync().Result[i]);
                    }
                }

            }
        }
        private void GetShopNames()
        {
            var dbBYS = App.DbBYS;
            MyShop myShopDefault = new MyShop
            {
                Name = "Standard",
                Tag = "Standard0123456789",
                Lab1 = "Tee/Kaffee/Brot",
                Lab2 = "Snacks",
                Lab3 = "Teig-/Trockenware",
                Lab4 = "Tiernahrung",
                Lab5 = "Getränke",
                Lab6 = "Drogerie",
                Lab7 = "Tiefkühlwaren",
                Lab8 = "Fleisch/Wurst/Fisch",
                Lab9 = "Milchprodukte",
                Lab10 = "Obst & Gemüse",
                Lab11 = "Sonstiges"
        };
            if (dbBYS.GetAllItemsAsync().Result.Count > 0)
            {
                myShopNames.Add(myShopDefault);

                for (int i = 0; i < dbBYS.GetDBCount().Result; i++)
                {
                    myShopNames.Add(dbBYS.GetAllItemsAsync().Result[i]);
                }
                MyShopList();
            }
           
        }

        private void GetListNames(bool edit, bool delete)
        {
            var dbLists = App.DbLists;
            
            if (dbLists.GetAllItemsAsync().Result.Count > 0)
            {
               
                for (int i = 0; i < dbLists.GetDBCount().Result; i++)
                {
                   shopListNames.Add(dbLists.GetAllItemsAsync().Result[i]);
                }
                ListNamesView(edit, delete);
            }
            else
            {
                CreateLabel("Keine Liste vorhanden", LayoutOptions.CenterAndExpand, LayoutOptions.CenterAndExpand, string.Empty, 3);
            }
        }
        #endregion Database
        #region Next Step
        private void ListMyShops_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as MyShop;

            //hier muss nun die reihenfolge der Kategorien der Liste hinzugefügt werden 
            ShowList(savedLists, itemData);
            listMyShops.SelectedItem = null;
        }
        private void ShowList(SavedLists itemData, MyShop myShop)
        {
            //mit myShop wird die gewünschte reihenfolge der kategorien festgelegt
            LoadListFromDB(itemData.Tag);
            //ListItemView muss demnach geändert werden
            //ListItemView(itemData.ListName, shopitem);
            Categories categories = new Categories(grid, myShop, mainPage, shopListItems, strikedItemList,itemData, this, shopitem);
            categories.SetCategories();
            SetInfoToDoDone();
        }
        #endregion Next Step
        //Testing area
        private static void SelectAllFromDb()
        {
            for (int i = 0; i < App.Db.GetAllItemsAsync().Result.Count; i++)
            {
                int id = App.Db.GetAllItemsAsync().Result[i].Id;
                ShopItems art = App.Db.GetItemAsync(id).Result;
                Debug.WriteLine(art.ItemName + " " + art.Id);
            }
        }
    }
}
