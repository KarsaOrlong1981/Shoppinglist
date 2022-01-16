using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppinglist.Models
{
    public class SavedLists
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ListName { get; set; }
        public string ListColor { get; set; }
        public string Tag { get; set; }
    }
}
