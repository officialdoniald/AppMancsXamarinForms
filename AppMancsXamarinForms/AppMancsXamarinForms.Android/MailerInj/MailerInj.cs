using AppMancsXamarinForms.BLL.Helper;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.Droid.MailerInj.MailerInj))]
namespace AppMancsXamarinForms.Droid.MailerInj
{
    public class MailerInj : IMailerInj
    {
        public string SendMail(string EMAIL, string NEWPW)
        {
            DAL.Mailer mailer = new DAL.Mailer();

            return mailer.SendEmail(EMAIL, NEWPW);
        }
    }
}
