using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace AppMancsXamarinForms.LocalDB
{
    public class LocalDatabaseTable
    {
        readonly SQLiteAsyncConnection database;

        public LocalDatabaseTable(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<MyPetsList>().Wait();
            database.CreateTableAsync<LastIndex>().Wait();
        }

        public Task<List<MyPetsList>> GetMyPetsList()
        {
            return database.Table<MyPetsList>().ToListAsync();
        }

        public Task<int> InsertMyPetsList(MyPetsList mypet)
        {
            return database.InsertAsync(mypet);
        }

        public Task<int> DeleteMyPetList(MyPetsList mypet)
        {
            return database.DeleteAsync(mypet);
        }

        public int DeleteAllMyPetList()
        {
            var list = GetMyPetsList().Result;

            foreach (var item in list)
            {
                try
                {
                    database.DeleteAsync(item);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return 1;
        }

        public Task<int> GetLastIndex(LastIndex lastIndex)
        {
            if (database.Table<LastIndex>().ToListAsync() is null)
            {
                return database.InsertAsync(lastIndex);
            }
            else
            {
                return database.UpdateAsync(lastIndex);
            }
        }



        //public Task<List<MyPetsList>> GetMyPetsListWhere(int id)
        //{
        //    return database.Table<MyPetsList>().Where(i => i.id == id).ToListAsync();
        //}

        //public Task<List<TodoItem>> GetItemsNotDoneAsync()
        //{
        //    return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}
    }
}
