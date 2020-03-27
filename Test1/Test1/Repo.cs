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
            _database.CreateTableAsync<WordModel>().Wait();
        }

        public Task<List<WordModel>> GetItemsAsync()
        {
            return _database.Table<WordModel>().ToListAsync();
        }

        public Task<int> AddItemAsync(WordModel newWord)
        {
            return _database.InsertAsync(newWord);
        }

        public Task<int> UpdateItemAsync(WordModel updateWord)
        {
            return _database.UpdateAsync(updateWord);
        }

        public Task<int> DeleteItemAsync(WordModel deleteWord)
        {
            return _database.DeleteAsync(deleteWord);
        }

        
    }        
}
     
