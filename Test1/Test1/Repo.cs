using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;

namespace Test1
{
    public class Repo
    {
        readonly SQLiteAsyncConnection _database;

        public Repo(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ItemModel>().Wait();
        }

        public Task<List<ItemModel>> GetItemsAsync()
        {
            return _database.Table<ItemModel>().ToListAsync();
        }

        public Task<int> AddItemAsync(ItemModel item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(ItemModel item)
        {
            return _database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(ItemModel item)
        {
            return _database.DeleteAsync(item);
        }
    }

    
}
