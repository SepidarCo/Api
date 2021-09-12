using Microsoft.AspNetCore.Http;

namespace Sepidar.Service.Models
{
    public class ContentModel
    {
        public long Id { get; set; }

        public IFormFile PictureFile { get; set; }

        public bool IsActive { get; set; }

        public long OrderNumber { get; set; }

        public long LanguageId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
