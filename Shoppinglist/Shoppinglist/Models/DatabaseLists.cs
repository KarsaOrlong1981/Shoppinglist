using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppinglist.Models
{
    public class DatabaseLists
    {
        readonly SQLiteAsyncConnection _database;
        public DatabaseLists(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SavedLists>().Wait();
        }
        public Task<List<SavedLists>> GetAllItemsAsync()
        {
            return _database.Table<SavedLists>().ToListAsync();
        }
        public Task<int> AddToDBAsync(SavedLists si)
        {
            return _database.InsertAsync(si);
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _database.Table<SavedLists>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
        public Task<SavedLists> GetItemAsync(int id)
        {
            return _database.Table<SavedLists>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<SavedLists>();
        }
        public Task<int> GetDBCount()
        {
            return _database.Table<SavedLists>().CountAsync();
        }
    }
}
