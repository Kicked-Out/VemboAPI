using Microsoft.AspNetCore.Http;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        public string UploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

        public string UploadFile(string directory, string fileName, IFormFile formFile)
        {
            string extension = Path.GetExtension(formFile.FileName);

            string relativePath = Path.Combine(directory, $"{fileName}{extension}");
            string path = Path.Combine(UploadRoot, relativePath);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return $"https://localhost:7213/assets/{relativePath.Replace("\\", "/")}";
        }
    }
}
