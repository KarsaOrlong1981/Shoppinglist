using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppinglist.Models
{
    public class DatabaseBuildYourShop
    {
        readonly SQLiteAsyncConnection _database;
        public DatabaseBuildYourShop(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MyShop>().Wait();
        }
        public Task<List<MyShop>> GetAllItemsAsync()
        {
            return _database.Table<MyShop>().ToListAsync();
        }
        public Task<int> AddToDBAsync(MyShop ms)
        {
            return _database.InsertAsync(ms);
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _database.Table<MyShop>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
        public Task<MyShop> GetItemAsync(int id)
        {
            return _database.Table<MyShop>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<MyShop>();
        }
        public Task<int> GetDBCount()
        {
            return _database.Table<MyShop>().CountAsync();
        }
    }
}
