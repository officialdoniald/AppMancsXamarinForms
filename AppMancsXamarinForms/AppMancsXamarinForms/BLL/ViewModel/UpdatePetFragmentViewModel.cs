using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class UpdatePetFragmentViewModel
    {
        public string DeletePet(int petid)
        {
            Pet pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeletePet(pet);

            GlobalVariables.LocalSQLiteDatabase.DeleteMyPetList(GlobalVariables.ConvertPetToMyPetList(pet));

            GlobalVariables.GetMyPets();
            GlobalVariables.InitializeTheMyPetList();
            GlobalVariables.AddedPet = true;

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }

        public string UpdatePetProfile(Pet pet)
        {
            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdatePet(pet.id, pet);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                GlobalVariables.LocalSQLiteDatabase.UpdateMyPetList(pet);

                GlobalVariables.InitializeTheMyPetList();

                GlobalVariables.AddedPet = true;

                return English.Empty();
            }
        }

        public async Task<string> UpdatePetProfilePictureAsync(Pet pet, Stream stream, string uri)
        {
            if (!String.IsNullOrEmpty(uri))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(uri, stream);

                uniqueBlobName = GlobalVariables.blobstorageurl + uniqueBlobName;

                pet.ProfilePictureURL = uniqueBlobName;

                bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdatePet(pet.id, pet);

                if (!success)
                {
                    return English.SomethingWentWrong();
                }
                else
                {
                    return English.Empty();
                }
            }
            else return English.Empty();
        }

        public Pet GetThisPet(int petid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);
        }
    }
}
