using Shoppinglist.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shoppinglist.ViewModels
{
    public class MainPageViewModel : BaseVM
    {
        #region Fields
        Grid mainGrid,grid;
        Label lab_newList, lab_newListColor;
        Entry entryNewList, entryNewItem, entryDescript, entryNumbers;
        CheckBox checkBoxColor1, checkBoxColor2, checkBoxColor3, checkBoxColor4;
        Color backgroundColor, foregroundColor;
        Button btn_OK, btn_Ready;
        ListView listView;
        MainPage mainPage;
        TextDecorations decorations;
        ShopItems shopitem;
        public List<ShopItems> slist;
        short counter;
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
            get => slist;
            set => SetProperty(ref slist, value);
        }
        #endregion Property Change
        public INavigation Navigation { get; set; }
        public MainPageViewModel(INavigation navigation, Grid grid, Grid mainGrid, MainPage mainPage)
        {
            this.Navigation = navigation; 
            this.grid = grid;
            this.mainGrid = mainGrid;
            this.mainPage = mainPage;
            backgroundColor = Color.White;
            foregroundColor = Color.Black;
            grid.BackgroundColor = backgroundColor;
            slist = new List<ShopItems>();
            MainMenue();
        }
        #region Create new List
        private void NewShoppingList()
        {
            //Name liste, farbe liste, 
            lab_newList = CreateLabel("Den Namen der Liste eingeben",LayoutOptions.StartAndExpand, LayoutOptions.CenterAndExpand, 0);
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
        private void CreateListView()
        {
            if (this.listView != null)
            {
                grid.Children.Remove(this.listView);
            }
            ListView listView = new ListView
            {
                ItemsSource = slist,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label lab = new Label();
                    lab.SetBinding(Label.TextProperty, "ItemName");
                    lab.FontSize = 16.0;
                    lab.TextColor = foregroundColor;
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
                                            VerticalOptions = LayoutOptions.Center,
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
            Grid.SetRow(listView, 1);
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
                    mainPage.BackgroundColor = Color.FromHex("#336b87");
                    grid.BackgroundColor = Color.FromHex("#336b87");
                    mainGrid.BackgroundColor = Color.FromHex("#336b87");
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#336b87");
                }
                if (checkBoxColor2.IsChecked)
                {
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#598234");
                    grid.BackgroundColor = Color.FromHex("#598234");
                    mainGrid.BackgroundColor = Color.FromHex("#598234");
                    mainPage.BackgroundColor = Color.FromHex("#598234");

                }
                if (checkBoxColor3.IsChecked)
                {
                    foregroundColor = Color.White;
                    backgroundColor = Color.FromHex("#07575B");
                    grid.BackgroundColor = Color.FromHex("#07575B");
                    mainGrid.BackgroundColor = Color.FromHex("#07575B");
                    mainPage.BackgroundColor = Color.FromHex("#07575B");

                }
                if (checkBoxColor4.IsChecked)
                {
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
        #region Events
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
            CreateToolBarItem();
            CheckBoxesColor();
            btn_Ready = new Button
            {
                Text = "Ready",
                BorderColor = Color.Black,
                BorderWidth = 4,
                TextColor = Color.Black,
                FontSize = 16.0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                IsEnabled = false
                
            };
            entryNewItem = CreateEntry("Artikel hinzufügen", LayoutOptions.StartAndExpand, LayoutOptions.FillAndExpand, 0);
            entryNewItem.Completed += EntryNewItem_Completed;
            mainGrid.Children.Add(btn_Ready);
        }
        private void EntryNewItem_Completed(object sender, EventArgs e)
        {
            string txt = "-  " + entryNewItem.Text;
            
            shopitem = new ShopItems();
            shopitem.ItemName = txt;
            shopitem.ListName = listName;
            shopitem.ListColor = backgroundColor;
            slist.Add(shopitem);
            CreateListView();
            
            entryNewItem.Text = "";
            entryNewItem.Focus();
        }
      
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemData = (sender as ListView).SelectedItem as ShopItems;
            slist.Remove(itemData);
            listView.SelectedItem = null;
            grid.Children.Remove(listView);
            CreateListView();

        }
        #endregion Events
        #endregion Create new List
        #region Main Menue
        private void MainMenue()
        {
            // auswahl neue liste erstellen,
            // bestand bereits angelegter listen einsehen und auswählen, 
            
            NewShoppingList();
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
            item.Clicked += Item_Clicked;
            mainPage.ToolbarItems.Add(item);
        }

        private void Item_Clicked(object sender, EventArgs e)
        {
            mainPage.BackgroundColor = Color.White;
            grid.BackgroundColor = Color.White;
            mainGrid.BackgroundColor = Color.White;
            backgroundColor = Color.White;
            foregroundColor = Color.Black;
            mainPage.ToolbarItems.Clear();
            mainPage.Title = "Hauptmenue";
            grid.Children.Clear();
            btn_Ready.IsVisible = false;
            entryNewItem.IsVisible = false; 
            MainMenue();
        }

        #endregion Main Menue
    }
}
