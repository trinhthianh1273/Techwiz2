namespace SoccerManager.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file, String pattern);

        Task<string> DeleteFile(string fileName, String pattern);
    }
}
