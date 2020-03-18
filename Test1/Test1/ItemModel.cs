using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Test1
{
   public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
