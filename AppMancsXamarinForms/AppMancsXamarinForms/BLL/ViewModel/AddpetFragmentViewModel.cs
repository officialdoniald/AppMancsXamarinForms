using AppMancsXamarinForms.BLL.Languages;
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
    public class AddpetFragmentViewModel
    {
        private English language = new English();
        
        public async Task<string> AddPetAsync(string EMAIL, string pathf, Stream f, Pet pet)
        {
            if (String.IsNullOrEmpty(pet.Name) || pet.Age < 0 || String.IsNullOrEmpty(pet.PetType))
            {
                return language.YouHaveToFillAllEntries();
            }
            else if (pet.Age < 0)
            {
                return language.NotNegNumber();
            }
            else if (!String.IsNullOrEmpty(pathf))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(pathf, f);

                uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

                pet.ProfilePictureURL = uniqueBlobName;
            }
            else pet.ProfilePictureURL = "";

            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            pet.Uploader = user.id;

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertPet(pet);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }
    }
}
