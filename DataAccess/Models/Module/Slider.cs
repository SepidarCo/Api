using System;

namespace Sepidar.Api.DataAccess.Models.Module
{
    public class Slider
    {
        public long Id { get; set; }

        public long? LanguageId { get; set; }

        public string Title { get; set; }

        public long OrderNumber { get; set; }

        public string PictureToken { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string LinkUrl { get; set; }
    }
}
