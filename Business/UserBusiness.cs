using Sepidar.Api.DataAccess.Models.Security;
using Sepidar.BlobManagement;
using Sepidar.Business.Interfaces;
using Sepidar.EntityFramework;
using Sepidar.Framework;
using Sepidar.Framework.Extensions;
using System;
using System.Linq;

namespace Sepidar.Business
{
    public class UserBusiness : IUserBusiness
    {
        private Repository<User> userRepository;
        private IMediaBusiness mediaBusiness;
        private IAdminBusiness adminBusiness;
        private IEndUserBusiness endUserBusiness;
        private IEmailHelper emailBusiness;

        public UserBusiness(Repository<User> userRepository, IMediaBusiness mediaBusiness, IAdminBusiness adminBusiness, IEndUserBusiness endUserBusiness, IEmailHelper emailBusiness)
        {
            this.userRepository = userRepository;
            this.mediaBusiness = mediaBusiness;
            this.adminBusiness = adminBusiness;
            this.endUserBusiness = endUserBusiness;
            this.emailBusiness = emailBusiness;
        }

        public void Create(Blob pictureBlob, string fullName, string username, string password, string mobileNumber, string email)
        {
            var token = mediaBusiness.Add(pictureBlob);

            var user = new User
            {
                FullName = fullName,
                Username = username,
                Password = password.Hash(),
                MobileNumber = mobileNumber,
                PictureToken = token,
                Email = email,
                CreationDate = DateTime.Now
            };

            using var transaction = TransactionScopeFactory.CreateTrasnactionScope();
            userRepository.Create(user);
            adminBusiness.Create(user.Id);
            transaction.Complete();
        }

        public void Update(long id, Blob pictureBlob, string fullName, string userName, string password, string mobileNumber, string email)
        {
            var user = userRepository.Get(id);

            user.FullName = fullName;
            user.Username = userName;
            user.Password = password;
            user.MobileNumber = mobileNumber;
            user.Email = email;

            if (pictureBlob.IsNotNull())
            {
                var token = mediaBusiness.Add(pictureBlob);
                user.PictureToken = token;
            }

            userRepository.Update(user);
        }

        public void Delete(long id)
        {
            using var transaction = TransactionScopeFactory.CreateTrasnactionScope();
            adminBusiness.Delete(id);
            userRepository.Delete(id);
            transaction.Complete();
        }

        public ListResult<User> List(ListOptions listOptions)
        {
            listOptions.AddSort<User>(i => i.Id, "desc");
            var list = userRepository.GetList(listOptions);
            return list;
        }

        public UserData Login(string userName, string password)
        {
            var passwordHash = password.Hash();
            var user = userRepository.All.FirstOrDefault(i => i.Username == userName);

            if (user.IsNull() || user.Password != passwordHash)
            {
                throw new FrameworkException("Username or Password is wrong!");
            }

            var userData = GetUserData(user, false, true);
            return userData;
        }

        private UserData GetUserData(User user, bool isEndUser, bool isAdmin)
        {
            var userData = new UserData
            {
                UserId = user.Id,
                IsEndUser = isEndUser,
                IsAdmin = isAdmin
            };

            return userData;
        }

        public dynamic Get(long id)
        {
            var user = userRepository.Get(id);

            if (user.IsNotNull())
            {
                dynamic picture = new
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    FullName = user.FullName,
                    MobileNumber = user.MobileNumber,
                    Email = user.Email,
                    CreationDate = user.CreationDate,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(user.PictureToken, (long?)MediaType.Image).ToString()
                };
                return picture;
            }
            else
            {
                throw new FrameworkException("User does not exists");
            }
        }

        public UserData SignIn(string email, string password)
        {
            var passwordHash = password.Hash();
            var user = userRepository.All.FirstOrDefault(i => i.Email == email);

            if (user.IsNull() || user.Password != passwordHash)
            {
                throw new FrameworkException("Username or Password is wrong!");
            }

            var userData = GetUserData(user, true, false);
            return userData;
        }

        public UserData Register(string email, string password, string rePassword)
        {
            var userExists = GetUserByEmail(email);
            if (userExists.IsNull())
            {
                if (password == rePassword)
                {
                    var user = new User
                    {
                        Password = password.Hash(),
                        Email = email,
                        CreationDate = DateTime.Now
                    };

                    using var transaction = TransactionScopeFactory.CreateTrasnactionScope();
                    userRepository.Create(user);
                    endUserBusiness.Create(user.Id);
                   // emailBusiness.Send("ali_fhl@yahoo.com");
                    transaction.Complete();

                    var userData = GetUserData(user, true, false);
                    return userData;
                }
                else
                {
                    throw new FrameworkException("Password is wrong!");
                }
            }
            else
            {
                throw new FrameworkException("Email already registered!");
            }
        }

        private User GetUserByEmail(string email)
        {
            var user = userRepository.Get(i => i.Email == email);
            return user;
        }

    }
}
