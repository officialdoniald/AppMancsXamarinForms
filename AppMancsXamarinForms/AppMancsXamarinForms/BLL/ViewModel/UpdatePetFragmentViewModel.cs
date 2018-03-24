using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.LocalDB;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class UpdatePetFragmentViewModel
    {
        public string DeletePet(int petid)
        {
            var petpictureList = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetpictureByID(petid);

            foreach (var item in petpictureList)
            {
                bool successD = GlobalVariables.seePictureFragmentViewModel.DeletePicture(item);

                if (!successD)
                {
                    return English.SomethingWentWrong();
                }
            }

            Pet pet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(i => i.petid == petid).FirstOrDefault());

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeletePet(pet);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                GlobalVariables.Mypetlist = new List<LocalDB.MyPetsList>();

                GlobalVariables.GetMyPets();

                GlobalVariables.SetMyPetListString();

                GlobalVariables.AddedPet = true;

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
                    await GlobalVariables.LocalSQLiteDatabase.UpdateMyPetList(pet);

                    GlobalVariables.InitializeTheMyPetList();

                    GlobalVariables.AddedPet = true;

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
