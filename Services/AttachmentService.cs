
namespace Bookstore.Services
{
    public class AttachmentService : IAttachmecntService
    {
        private List<string> Extentions = [".JPEG", ".GIF", ".PNG", ".JPG"];
        private const int MaxSize = 1024 * 1024 * 10; // 10 Miga bytes

        public void Delete(string fileName , string FolderName)
        {
            //if (fileName is null || fileName.Equals(string.Empty)) return;

            string fullPath = Path.Combine(FolderName, fileName);

            if (!File.Exists(fullPath)) 
                return;
            else
                File.Delete(fullPath);
        }

        public string? Create(IFormFile File , string FolderName)
        {
           if( File is null ) return null;

            // check extention and size : 
            var fileExten = Path.GetExtension(File.FileName).ToUpper();
            if(!Extentions.Contains(fileExten) ) return null;
            if(File.Length > MaxSize || File.Length == 0 ) return null;

            //make unique filename
            var fileName = $"{Guid.NewGuid()}_{File.FileName}";

            // save file

            var fullPath = Path.Combine(FolderName, fileName);
            using var fileStream = new FileStream(fullPath, FileMode.Create);
            File.CopyTo(fileStream);

            return fileName;
        }
    }
}
