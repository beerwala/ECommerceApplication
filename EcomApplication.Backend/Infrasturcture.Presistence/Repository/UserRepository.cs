using Application.DTO;
using Application.Interface.Repository;
using Domain.Model;
using Application.Wrappper;
using Infrasturcture.Presistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrasturcture.Presistence.Services;

namespace Infrasturcture.Presistence.Repository
{
    public class UserRepository:GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly DbSet<ApplicationUser> user;
        private readonly ApplicationContext _dbContext;
        private readonly EncryptionService _encryptionService;

        public UserRepository(ApplicationContext application,
                                EncryptionService encryptionService) :base(application)
        {
            this.user = application.Set<ApplicationUser>();
            _dbContext = application;
            _encryptionService = encryptionService;
        }

        public Task ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public Task LoginUser(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public async Task UserRegistration(RegisterUserDTO model)
        {
            
           
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserType = model.UserType,
                    DOB = model.DOB,
                    Mobile = model.Mobile,
                    Address = model.Address,
                    Zipcode = model.Zipcode,
                    StateId = model.StateId,
                    CountryId = model.CountryId
                };

                //Generating-Custom UserName& Password
                var username = GenerateUsername(user);
                var password = GenerateRandomPassword();

                //Encrpting Data
                //var encryptedPassword = _encryptionService.Encrypt(password);
                //var encryptedUserName = _encryptionService.Encrypt(username);

                //user.Password = encryptedPassword;
                //user.Username = encryptedUserName;

                await _dbContext.users.AddAsync(user);
                _dbContext.SaveChanges();
          
          
            //var userName = GetUserByUsername(username);
            //if (user != null)
            //{
            //    var decryptedPassword = _encryptionService.Decrypt(user.Password);
            //    // Use decryptedPassword for comparison during login
            //    // Similarly, decrypt any other sensitive data retrieved from the database
            //}

        }


        private string GenerateUsername(ApplicationUser model)
        {
            var dobFormat = model.DOB.ToString("ddMMyy");
            var username = $"ES_{model.LastName.ToUpper()}{model.FirstName.ToUpper()[0]}{dobFormat}";
            return username;
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }




        public ApplicationUser GetUserByUsernameAndPassword(string username, string password)
        {
            var encryptedUsername = _encryptionService.Encrypt(username);
            var encryptedPassword = _encryptionService.Encrypt(password);

            return user.FirstOrDefault(u => u.EncryptedUsername == encryptedUsername && u.EncryptedPassword == encryptedPassword);
        }
        public ApplicationUser GetUserByUsername(string username)
        {
            var encryptedUsername = _encryptionService.Encrypt(username);
            return _dbContext.users.FirstOrDefault(u => u.EncryptedUsername == encryptedUsername);
        }
    }
}
