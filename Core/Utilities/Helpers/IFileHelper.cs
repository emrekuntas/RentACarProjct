using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
     public interface IFileHelper
    {
        Task<IResult> Upload(IFormFile file,string root);
        void Delete(string filePath);
        IResult Update(IFormFile file, string original,string root);

    }
}
