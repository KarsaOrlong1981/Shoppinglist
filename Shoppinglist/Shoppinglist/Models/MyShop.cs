using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppinglist.Models
{
    public class MyShop
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
       
        public string Lab1 { get; set; }
        public string Lab2 { get; set; }
        public string Lab3 { get; set; }
        public string Lab4 { get; set; }
        public string Lab5 { get; set; }
        public string Lab6 { get; set; }
        public string Lab7 { get; set; }
        public string Lab8 { get; set; }

    }
}
