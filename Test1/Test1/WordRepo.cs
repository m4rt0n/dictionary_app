using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public class WordRepo
    {
        readonly static string _connStr = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");
        readonly static SQLiteAsyncConnection _database = new SQLiteAsyncConnection(_connStr);

        public WordRepo()
        {
            _database.CreateTableAsync<Word>().Wait();
        }

        public Task<List<Word>> GetAllItemsAsync()
        {
            return _database.Table<Word>().ToListAsync();
        }

        public Task<int> AddItemAsync(Word word)
        {
            return _database.InsertAsync(word);
        }

        public Task<int> UpdateItemAsync(Word word)
        {
            return _database.UpdateAsync(word);
        }

        public Task<int> DeleteItemAsync(Word word)
        {
            return _database.DeleteAsync(word);
        }
    }
}