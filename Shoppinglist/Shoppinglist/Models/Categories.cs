using Shoppinglist.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shoppinglist.Models
{
    public class Categories
    {
        private Grid grid, categorieGrid;
        private MyShop myShop;
        private MainPage mainPage;
        private List<ShopItems> shopListItems, strikedItemList;
        private SavedLists savedLists;
        private MainPageViewModel mainPageViewModel;
        private ListView listItemView, listViewStrikedItem;
        private ShopItems shopItems;
        public Categories(Grid grid, MyShop myShop, MainPage mainPage, List<ShopItems> shopListItems, List<ShopItems> strikedItemList, SavedLists savedLists, MainPageViewModel mainPageViewModel, ShopItems shopItems)
        {
            this.grid = grid;
            this.myShop = myShop;
            this.mainPage = mainPage;
            this.shopListItems = shopListItems;
            this.strikedItemList = strikedItemList;
            this.savedLists = savedLists;
            this.mainPageViewModel = mainPageViewModel;
            this.shopItems = shopItems;
            grid.Children.Clear();
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
        public void SetCategories()
        {
            NewCategorieGrid();
            ListItemView(shopItems,savedLists.ListName, myShop.Lab1, 0);
            ListItemView(shopItems,savedLists.ListName, myShop.Lab2, 1);
            ListItemView(shopItems,savedLists.ListName, myShop.Lab3, 2);
            ListItemView(shopItems,savedLists.ListName, myShop.Lab4, 3);
            ListItemView(shopItems,savedLists.ListName, myShop.Lab5, 4);
            ListItemView(shopItems,savedLists.ListName, myShop.Lab6, 5);
            ListItemView(shopItems, savedLists.ListName, myShop.Lab7, 6);
            ListItemView(shopItems, savedLists.ListName, myShop.Lab8, 7);
            ListItemView(shopItems, savedLists.ListName, myShop.Lab9, 8);
            ListItemView(shopItems, savedLists.ListName, myShop.Lab10, 9);
            ListItemView(shopItems, savedLists.ListName, myShop.Lab11, 10);
            ListStrikedItems(shopItems);
        }
        private void ListItemView(ShopItems si,string title, string categorie, short row)
        {
           
            mainPage.Title = title;

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
                ItemsSource = shopListItems,
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
            Grid.SetRow(lab_Categorie, 0);
            Grid.SetRow(listItemView, 1);
            gridStricked.Children.Add(lab_Categorie);
            gridStricked.Children.Add(listItemView);
            Grid.SetRow(gridStricked, row);
            categorieGrid.Children.Add(gridStricked);
           
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
            mainPageViewModel.StrikedItemsToDO(sender, listViewStrikedItem, myShop, savedLists);
        }

        private void ListItemView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            mainPageViewModel.ItemTappedDone(sender, listItemView, myShop, savedLists);
        }
    }
}
