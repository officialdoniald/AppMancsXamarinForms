using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMancsXamarinForms.BLL.Languages
{
    public class English : ILanguage
    {
        public string AlreadyHaveThisFacebookAcoount()
        {
            return "Van már ilyen Facebook profillel felhasználó!";
        }
        public string NoAccountFindWithThisFacebookAccount()
        {
            return "Nem található ilyen Facebook felhasználóval fiók az adatbázisunkban!";
        }
        public string AppName()
        {
            return "AppMancs";
        }

        public string BadOldPassword()
        {
            return "Nem megfelelő a régi jelszó!";
        }

        public string BadPasswordLength()
        {
            return "Jelszavad 6-16 hosszúságúnak kell lenni!";
        }

        public string ChooseAnimal()
        {
            return "Válassz háziállatot!";
        }

        public string ChooseAPicture()
        {
            return "Válassz fényképet!";
        }

        public string Empty()
        {
            return "";
        }

        public string SomethingWentWrong()
        {
            return "Valami hiba történt, kérlek próbáld újra később!";
        }

        public string ThisEmailIsExist()
        {
            return "Van már ilyen E-Mail címmel felhasználó!";
        }

        public string ThisEmailIsYourEmail()
        {
            return "Ez az E-Mail cím a mostanid!";
        }

        public string YouHaveToFillAllEntries()
        {
            return "Tölts ki minden mezőt!";
        }


        public string Like()
        {
            return "Tetszik";
        }

        public string UnLike()
        {
            return "Nem tetszik";
        }

        public string Follow()
        {
            return "Követem";
        }

        public string UnFollow()
        {
            return "Nem követem";
        }

        public string GiveYourEmail()
        {
            return "Add meg az E-Mail címedet!";
        }
        public string NoAcoountFoundWithThisEmail()
        {
            return "Nincs ilyen felhasználó az adatbázisunkban!";
        }

        public string EmailIsEmpty()
        {
            return "E-Mail cím üres!";
        }

        public string PasswordIsEmpty()
        {
            return "E-Mail cím üres!";
        }

        public string BadEmailFormat()
        {
            return "Biztos E-Mail címet adtál meg?";
        }

        public string BadPasswordOrEmail()
        {
            return "Rossz E-Mail cím jelszó páros!";
        }

        public string NotNegNumber()
        {
            return "Ne legyen már negatív szám!";
        }
    }
}
