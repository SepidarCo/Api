using System;

namespace Sepidar.Api.DataAccess.Models.Module
{
    public class Calendar
    {
        public long Id { get; set; }

        public long? LanguageId { get; set; }

        public long OrderNumber { get; set; }

        public string Title { get; set; }

        public string ShortDate { get; set; }

        public string Date { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public string LinkUrl { get; set; }
    }
}
