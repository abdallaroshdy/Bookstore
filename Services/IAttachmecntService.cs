namespace Bookstore.Services
{
    public interface IAttachmecntService
    {
        public void Delete(string fileName, string FolderName);
        public string? Create(IFormFile File , string FolderName);
    }
}
