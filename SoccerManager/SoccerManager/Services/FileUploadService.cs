using SoccerManager.Interfaces;

namespace SoccerManager.Services
{
    public class FileUploadService : IFileUploadService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file, String pattern)
        {
            string path = "";
            try
            {
                path = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //generate unique file name
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string uniqueFileName = $"{pattern}_{fileNameWithoutExtension}_{Guid.NewGuid()}{fileExtension}";

                //save image to folder
                using (var fileStream = new FileStream(Path.Combine(path, uniqueFileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}