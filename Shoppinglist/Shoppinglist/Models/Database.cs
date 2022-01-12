using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Shoppinglist.Models
{
   
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopItems>().Wait();
        }
        public Task<List<ShopItems>> GetAllItemsAsync()
        {
            return _database.Table<ShopItems>().ToListAsync();
        }
        public Task<int> AddToDBAsync(ShopItems si)
        {
            return _database.InsertAsync(si);
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _database.Table<ShopItems>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
        public Task<ShopItems> GetItemAsync(int id)
        {
            return _database.Table<ShopItems>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<ShopItems>();
        }
        public Task<int> GetDBCount()
        {
            return _database.Table<ShopItems>().CountAsync();
        }
    }
}
