using System;
using System.Collections.Generic;
using System.IO;
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
        }

        public Task<List<MyPetsList>> GetMyPetsList()
        {
            return database.Table<MyPetsList>().ToListAsync();
        }

        //public Task<List<MyPetsList>> GetMyPetsListWhere(int id)
        //{
        //    return database.Table<MyPetsList>().Where(i => i.id == id).ToListAsync();
        //}

        public Task<int> InsertMyPetsList(MyPetsList mypet)
        {
            return database.InsertAsync(mypet);
        }

        public Task<int> DeleteMyPetList(MyPetsList mypet)
        {
            return database.DeleteAsync(mypet);
        }

        public Task<int> DeleteAllMyPetList()
        {
            return database.DropTableAsync<MyPetsList>();
        }
    }
}
