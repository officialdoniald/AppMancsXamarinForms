using System;
using System.IO;
using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppMancsXamarinForms.iOS.LocalFilePath.LocalFileHelper))]
namespace AppMancsXamarinForms.iOS.LocalFilePath
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}