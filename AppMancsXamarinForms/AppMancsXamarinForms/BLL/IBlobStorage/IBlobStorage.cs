using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMancsXamarinForms.BLL.IBlobStorage
{
    public interface IBlobStorage
    {
        Task<List<Uri>> GetAllBlobUrisAsync();

        Task<string> UploadFileAsync(string filepath, Stream file);
    }
}
