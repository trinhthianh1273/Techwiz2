namespace SoccerManager.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file, String pattern);
    }
}
