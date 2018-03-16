using System;
using System.IO;
using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppMancsXamarinForms.Droid.LocalFilePath.LocalFileHelper))]
namespace AppMancsXamarinForms.Droid.LocalFilePath
{
    public class LocalFileHelper:ILocalFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
