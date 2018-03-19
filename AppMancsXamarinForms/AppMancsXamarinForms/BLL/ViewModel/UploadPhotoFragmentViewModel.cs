using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Model;
using System.IO;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class UploadPhotoFragmentViewModel
    {
        public List<Pet> GetMyPets(int userid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetsByUserID(userid);
        }

        public User GetUserByEmail(string userEmail)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);
        }

        public async System.Threading.Tasks.Task<string> UploadPictureAsync(string pathf, Stream f, int petid, string hashtag)
        {
            if (petid == -1)
            {
                return English.ChooseAnimal();
            }
            if (!String.IsNullOrEmpty(pathf))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(pathf, f);

                uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

                Petpictures petpictures = new Petpictures()
                {
                    PetID = petid,
                    PictureURL = uniqueBlobName,
                    UploadDate = DateTime.UtcNow.ToString("")
                };

                int success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertPetpictures(petpictures);

                if (success == -1)
                {
                    return English.SomethingWentWrong();
                }
                else
                {
                    if (!String.IsNullOrEmpty(hashtag))
                    {
                        var hashtags = hashtag.Split(' ');

                        bool asd = true;

                        foreach (var item in hashtags)
                        {
                            if (item[0] == '#')
                            {
                                Hashtags ahashtag = new Hashtags();

                                ahashtag.hashtag = item.Remove(0, 1);
                                ahashtag.petpicturesid = success;

                                asd = DependencyService.Get<IDBAccess.IBlobStorage>().InsertHashtags(ahashtag);

                                if (!asd)
                                {
                                    return English.SomethingWentWrong();
                                }
                            }
                        }
                    }

                    return English.Empty();
                }
            }
            else
            {
                return English.ChooseAPicture();
            }


        }
    }
}
