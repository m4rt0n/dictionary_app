using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Test1
{
   public class WordModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string WordEng { get; set; }
        public string WordRus { get; set; }
        public string WordNote { get; set; }
        public string WordPicturePath { get; set; }       
    }
}
