using AppMancsXamarinForms.BLL.Helper;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.iOS.MailerInj.MailerInj))]
namespace AppMancsXamarinForms.iOS.MailerInj
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
