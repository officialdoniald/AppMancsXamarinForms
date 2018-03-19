﻿using AppMancsXamarinForms.BLL.Helper;
using Model;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class AddpetFragmentViewModel
    {
        public async Task<string> AddPetAsync(string pathf, Stream f, Pet pet)
        {
            if (String.IsNullOrEmpty(pet.Name) || pet.Age < 0 || String.IsNullOrEmpty(pet.PetType))
            {
                return English.YouHaveToFillAllEntries();
            }
            else if (pet.Age < 0)
            {
                return English.NotNegNumber();
            }
            else if (!String.IsNullOrEmpty(pathf))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(pathf, f);

                uniqueBlobName = GlobalVariables.blobstorageurl + uniqueBlobName;

                pet.ProfilePictureURL = uniqueBlobName;
            }
            else pet.ProfilePictureURL = "";

            pet.Uploader = GlobalVariables.ActualUser.id;

            int success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertPet(pet);

            if (success == -1)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                pet.id = success;

                var myPetList = GlobalVariables.ConvertPetToMyPetList(pet);

                GlobalVariables.Mypetlist.Add(myPetList);

                GlobalVariables.SetMyPetListString();

                GlobalVariables.AddedPet = true;

                await GlobalVariables.LocalSQLiteDatabase.InsertMyPetsList(myPetList);
                
                return English.Empty();
            }
        }
    }
}
