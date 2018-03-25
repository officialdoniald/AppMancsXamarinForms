using System;
using System.Collections.Generic;
using System.Linq;
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
            database.CreateTableAsync<LastIndex>().Wait();
            //database.CreateTableAsync<MyWall>().Wait();
            //database.CreateTableAsync<PetpicturesWall>().Wait();
            //database.CreateTableAsync<WallItem>().Wait();
        }


        public Task<int> DropTableMyPetlist()
        {
            var returnableInt =  database.DropTableAsync<MyPetsList>();

            database.CreateTableAsync<MyPetsList>().Wait();

            return returnableInt;
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

        public async Task<int> UpdateMyPetList(Pet pet)
        {
            var mypetlist = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(i => i.petid == pet.id).FirstOrDefault());

            var convertedNewPet = GlobalVariables.ConvertPetToMyPetList(pet);

            convertedNewPet.id = mypetlist.id;

            return  await database.UpdateAsync(convertedNewPet);
        }

        public Pet GetPetFromsMypetlist(int petid)
        {
            var pet = database.Table<MyPetsList>().Where(i => i.petid == petid).FirstOrDefaultAsync();
            
            return GlobalVariables.ConvertMyPetListToPet(pet.Result);
        }

        //LastIndex
        public Task<int> SetGetLastIndex(LastIndex lastIndex)
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

        //WallItem
        public Wall GetWallItemById(int wallitemid)
        {
            return GlobalVariables.ConvertWallItemToWall(database.Table<WallItem>().Where(i => i.id == wallitemid).FirstOrDefaultAsync().Result);
        }

        public Task<List<WallItem>> GetWallItemList()
        {
            return database.Table<WallItem>().ToListAsync();
        }

        public Task<int> InsertWallItem(WallItem wallItem)
        {
            return database.InsertAsync(wallItem);
        }

        public Task<int> DeleteWallItem(WallItem wallItem)
        {
            return database.DeleteAsync(wallItem);
        }

        public int DeleteAllWallItem()
        {
            var list = GetWallItemList().Result;

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

        //PetpicturesWall
        public Petpictures GetPetpicturesWallById(int petpicturesid)
        {
            return GlobalVariables.ConvertPetPicturesWallToPetpictures(database.Table<PetpicturesWall>().Where(i => i.petpicturesid == petpicturesid).FirstOrDefaultAsync().Result);
        }

        public Task<List<PetpicturesWall>> GetPetpicturesWallList()
        {
            return database.Table<PetpicturesWall>().ToListAsync();
        }

        public Task<int> InsertPetpicturesWall(PetpicturesWall petpicturesWall)
        {
            return database.InsertAsync(petpicturesWall);
        }

        public Task<int> DeletePetpicturesWall(PetpicturesWall petpicturesWall)
        {
            return database.DeleteAsync(petpicturesWall);
        }

        public int DeleteAllPetpicturesWall()
        {
            var list = GetPetpicturesWallList().Result;

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
