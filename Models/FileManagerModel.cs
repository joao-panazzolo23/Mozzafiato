namespace Mozzafiato.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile formFile { get; set; }
        public List<IFormFile> formFilesList { get; set; }
        public string PathImagensProdutos{get;set; }
    }
}
