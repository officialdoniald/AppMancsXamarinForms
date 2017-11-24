﻿using AppMancsXamarinForms.BLL.Languages;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class UpdatePetFragmentViewModel
    {
        private English language = new English();

        public string DeletePet(int petid)
        {
            Pet pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeletePet(pet);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }

        public string UpdatePetProfile(Pet pet)
        {
            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdatePet(pet.id, pet);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }

        public async Task<string> UpdatePetProfilePictureAsync(Pet pet, Stream stream, string uri)
        {
            if (!String.IsNullOrEmpty(uri))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(uri, stream);

                uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

                pet.ProfilePictureURL = uniqueBlobName;

                bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdatePet(pet.id, pet);

                if (!success)
                {
                    return language.SomethingWentWrong();
                }
                else
                {
                    return language.Empty();
                }
            }
            else return language.Empty();
        }

        public Pet GetThisPet(int petid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);
        }
    }
}
