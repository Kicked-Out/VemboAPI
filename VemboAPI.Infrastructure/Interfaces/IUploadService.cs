using Microsoft.AspNetCore.Http;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUploadService
    {
        public string UploadFile(string directory, string fileName, IFormFile formFile);
    }
}
