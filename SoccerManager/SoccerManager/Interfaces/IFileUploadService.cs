namespace SoccerManager.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file, string dir, string pattern);

        Task<bool> DeleteFile(string fileName);
    }
}
