using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Shoppinglist.Models
{
    public class ShopItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ListName { get; set; }
        public string ItemName { get; set; }
        public Color ListColor { get; set; }
        public TextDecorations Decorations { get; set; }
    }
}
