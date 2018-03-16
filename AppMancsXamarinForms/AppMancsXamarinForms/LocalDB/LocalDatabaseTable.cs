using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppMancsXamarinForms.BLL.Helper;
using Model;
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
            database.CreateTableAsync<MyWall>().Wait();
        }

        //MyPetList
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

        public Task<int> UpdateMyPetList(Pet pet)
        {
            var mypetlist = database.Table<MyPetsList>().Where(i => i.petid == pet.id).ToListAsync().Result;

            var convertedNewPet = GlobalVariables.ConvertPetToMyPetList(pet);

            convertedNewPet.id = mypetlist[0].id;

            return database.UpdateAsync(convertedNewPet);
        }

        public Pet GetPetFromsMypetlist(int petid)
        {
            return GlobalVariables.ConvertMyPetListToPet(database.Table<MyPetsList>().Where(i => i.petid == petid).FirstOrDefaultAsync().Result);
        }

        //LastIndex
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

        //MyWall
        public Task<List<MyWall>> GetMyWallList()
        {
            return database.Table<MyWall>().ToListAsync();
        }

        public Task<int> InsertMyWall(MyWall myWall)
        {
            return database.InsertAsync(myWall);
        }

        public Task<int> DeleteMyWall(MyWall myWall)
        {
            return database.DeleteAsync(myWall);
        }

        public int DeleteAllMyWall()
        {
            var list = GetMyWallList().Result;

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

        //public Task<List<TodoItem>> GetItemsNotDoneAsync()
        //{
        //    return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}
    }
}
