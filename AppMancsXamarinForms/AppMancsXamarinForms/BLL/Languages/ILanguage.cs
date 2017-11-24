using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMancsXamarinForms.BLL.Languages
{
    public interface ILanguage
    {
        string AppName();

        string ChooseAnimal();

        string SomethingWentWrong();

        string ChooseAPicture();

        string Empty();

        string ThisEmailIsYourEmail();

        string ThisEmailIsExist();

        string YouHaveToFillAllEntries();

        string BadOldPassword();

        string BadPasswordLength();

        string Like();

        string UnLike();

        string Follow();

        string UnFollow();

        string GiveYourEmail();

        string NoAcoountFoundWithThisEmail();

        string EmailIsEmpty();

        string PasswordIsEmpty();

        string BadEmailFormat();

        string BadPasswordOrEmail();

        string NotNegNumber();
    }
}
