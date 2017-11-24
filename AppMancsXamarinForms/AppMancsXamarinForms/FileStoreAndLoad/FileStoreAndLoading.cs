using FileStoringWithDependency.IFileStoreAndLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.FileStoreAndLoad
{
    public class FileStoreAndLoading
    {
        public string GetSomethingText(string filename)
        {
            try
            {
                string returnString = DependencyService.Get<IFileStoreAndLoad>().LoadText(filename);

                return returnString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void InsertToFile(string filename, string text)
        {
            DependencyService.Get<IFileStoreAndLoad>().SaveText(filename, text);
        }
    }
}
