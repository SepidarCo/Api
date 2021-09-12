using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepidar.Service.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        public IFormFile PictureFile { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
