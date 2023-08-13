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

        public async Task<string> UploadFile(IFormFile file, string dir, string pattern)
        {
            try
            {
                var path = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath, "images/" + dir));
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
        
        public async Task<bool> DeleteFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
        
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false; // File doesn't exist
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Delete Failed", ex);
            }
        }
    }
}