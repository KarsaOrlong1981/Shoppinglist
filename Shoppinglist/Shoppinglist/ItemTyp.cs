using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppinglist
{
    public class ItemTyp
    {

        public ItemTyp()
        {

        }
        /*  private void buttonLos_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Visibility = Visibility.Hidden;
            List<string> searchResult = new List<string>();
            string input = txt_search .Text;
            input = input.ToLower ();
            foreach (var item in pokemonNames)
            {
                bool containsResult = item.Contains(input);
                if (containsResult)
                {
                   searchResult.Add(item); 
                }
            }
            ListView listView = new ListView()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 200,
                Margin = new Thickness(0,40,0,0),
            };
            listView.SelectionChanged += ListView_SelectionChanged;
            foreach (var item in searchResult)
            {
                listView.Items.Add(item);
            }
            this.listView = listView;
            this.grid.Children.Add(listView);  
            Grid.SetRow(listView, 0);
        }*/
    }
}
