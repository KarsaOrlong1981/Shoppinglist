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
        public string ListTag { get; set; }
        public string ItemName { get; set; }
       
        public bool ListCBIsChecked { get; set; }
       
        public string Tag { get; set; }
       
        [Ignore]
        public Color ListCBColor { get; set; }
        [Ignore]
        public CheckBox ListCheckBox { get; set; }
        [Ignore]
        public TextDecorations Decorations { get; set; }


    }
}
