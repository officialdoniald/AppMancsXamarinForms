using FileStoringWithDependency.IFileStoreAndLoad;
using System;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.FileStoreAndLoad
{
    public static class FileStoreAndLoading
    {
        public static void GetSomethingText(string filename)
        {
            try
            {
                GlobalVariables.ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(filename);
            }
            catch (Exception)
            {
                GlobalVariables.ActualUsersEmail = String.Empty;
            }
        }

        public static void InsertToFile(string filename, string text)
        {
            DependencyService.Get<IFileStoreAndLoad>().SaveText(filename, text);
        }
    }
}
