using System;

namespace Sepidar.Api.DataAccess.Models.Security
{
    public class User
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PictureToken { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
