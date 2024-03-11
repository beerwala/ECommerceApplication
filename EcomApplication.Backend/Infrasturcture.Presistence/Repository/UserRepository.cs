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
using System.Security.Cryptography;
using Infrasturcture.shared.Models;
using Infrasturcture.shared.Services;
using Microsoft.AspNetCore.Http;
using System.Net;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Interface;

namespace Infrasturcture.Presistence.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly DbSet<ApplicationUser> user;
        private readonly ApplicationContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly TwilioSettings _twilioSettings;
        private readonly IPictureHandler handler;


        public UserRepository(ApplicationContext application,
                               IEmailService emailService,
                               IConfiguration configuration,
                               IOptions<TwilioSettings> twilioSettings,
                               IPictureHandler picture                   
            ) : base(application)
        {
            this.user = application.Set<ApplicationUser>();
            _dbContext = application;   
            _emailService = emailService;
            _configuration = configuration;
            _twilioSettings = twilioSettings.Value;
            this.handler = picture;
        }



        public async Task<Response<string>> SendResetPasswordOTP(ForgotPasswordDTO model)
        {
            try
            {
                var userEmailValidate = _dbContext.users.FirstOrDefault(u => u.Email == model.Email);
                if (userEmailValidate != null)
                {
                    var storeOtp = GenerateOTP(); // Store the generated OTP
                    var message = new Message(new string[] { model.Email! }, "Password Reset OTP", $"Your OTP token is: {storeOtp}");
                    userEmailValidate.ResetPasswordOtp = storeOtp;
                    _dbContext.SaveChanges();
                  _emailService.SendEmail(message);
                }
                else
                {
                    // User not found, handle appropriately
                    // For example, throw an exception or log an error
                    throw new ArgumentException("User not found");
                }
                return new Response<string> { StatusCode = (int)HttpStatusCode.Unauthorized, Message = "Incorrect OTP." };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's requirements
                // For example, return a generic error message
                throw new Exception("An error occurred while sending reset password OTP. Please try again later.", ex);
            }
        }


        // OTP Validation and Password Reset method
        public async Task<Response<string>> ResetPassword(ValidateOtpDTO otpDTO)
        {
            var userEmailValidate = _dbContext.users.FirstOrDefault(u => u.Email == otpDTO.Email);
            if (userEmailValidate != null)
            {
                                                            // Verify OTP from user input
                if (userEmailValidate != null && userEmailValidate.ResetPasswordOtp == otpDTO.UserOTP)
                {
                    var password = GenerateRandomPassword();
                    var resetPasswordMsg = new Message(new string[] { otpDTO.Email! }, "Password Reset Successful", $"Your password has been successfully reset. Username: {userEmailValidate.Username} Updated Password: {password}");
                    _emailService.SendEmail(resetPasswordMsg);

                    // Encrypt and update password
                    byte[] bytesToEncodePass = Encoding.UTF8.GetBytes(password);
                    var encryptedPassword = Convert.ToBase64String(bytesToEncodePass);
                    var passwordHash = EncryptPassword(encryptedPassword);
                    userEmailValidate.Password = passwordHash;
                    _dbContext.SaveChanges();
                }
            }
            return new Response<string> { StatusCode = (int)HttpStatusCode.Unauthorized, Message = "Incorrect OTP." };
        }



        public async Task<Response<string>> LoginUser(LoginDTO login)
        {
            var user = _dbContext.users.FirstOrDefault(u => u.Username == login.Username);

            if (user != null)
            {
                bool vr = VerifyPassword(login.Password, user.Password);
                if (vr)
                {
                    // Generate OTP
                    var otp = GenerateOTP();

                    // Save OTP to the user's data in the database
                    user.LoginOtp = otp;
                    _dbContext.SaveChanges();

                    // Send OTP via SMS
                    await SendOTPviaSMS(user.Mobile, otp);

                    // Successful login
                    var jwtToken = GenerateJwtToken(user);

                    return new Response<string> {Succeeded=true, StatusCode = (int)HttpStatusCode.OK, Message = $"{login.Username} logged in successfully. OTP sent to your registered mobile number.", Data = jwtToken };
                }
                else
                {
                    // Incorrect password
                    return new Response<string> { StatusCode = (int)HttpStatusCode.Unauthorized, Message = "Incorrect password." };
                }
            }
            else
            {
                // User not found
                return new Response<string> { StatusCode = (int)HttpStatusCode.NotFound, Message = "User not found." };
            }
        }

        public async Task<Response<string>> AuthenticationLoginUser(string username,string code)
        {
            var user = _dbContext.users.FirstOrDefault(u => u.Username== username);
            if (user != null&&user.LoginOtp==code)
            {
                var jwtToken =  GenerateJwtToken(user);

                return  new  Response<string> {Succeeded=true, StatusCode = (int)HttpStatusCode.OK, Message = $"{user.Username} logged in successfully. OTP sent to your registered mobile number.", Data = jwtToken };
            }
            return new Response<string> { StatusCode = (int)HttpStatusCode.Unauthorized, Message = "Incorrect OTP Entered." };
        }




        public async Task<Response<int>> UserRegistration(RegisterUserDTO model)
        {
            var userEmail = _dbContext.users.FirstOrDefault(u => u.Email == model.Email);

            if (userEmail == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserType = model.UserType,
                    DOB = model.DOB,
                    Mobile = model.Mobile,
                    ProfileImage = handler.ConvertBase64ToImage(model.ProfileImage, "/wwwroot/Icon-Image/"),
                    Address = model.Address,
                    Zipcode = model.Zipcode,
                    StateId = model.StateId,
                    CountryId = model.CountryId
                };

                //Generating-Custom UserName& Password
                var username = GenerateUsername(user);
                var password = GenerateRandomPassword();
                // var profileImg= handler.ConvertBase64ToImage(model.ProfileImage, "/wwwroot/Icon-Image/");

                //Email-Intergration
                var content = $"Dear {user.FirstName} {user.LastName},\n\nWelcome to our community! We are thrilled to have you join us on this exciting journey of exploration and growth." +
                    $"Below are your login credentials:\n\nUsername: {username}\nPassword: {password}\n\n" +
                    $"\n\nOnce again, welcome aboard!\n\nBest regards,\nSushant Buche";

                var message = new Message(new string[] { user.Email! }, "E-Com Application Login Credential", content);
                _emailService.SendEmail(message);

                // converted into hash
                var passwordHash = EncryptPassword(password);

                user.Password = passwordHash;
                user.Username = username;

                await _dbContext.users.AddAsync(user);
                _dbContext.SaveChanges();
               

                return new Response<int> { StatusCode = (int)HttpStatusCode.Created, Message = $"{model.Email} this user is Registered succesfully." };
            }
            else if (userEmail != null)
            {
                return new Response<int> { StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"{model.Email} is Already register" };
            }
            else
            {
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Something went wrong.Try Again"
                };
            }



        }

        public async Task<Response<bool>> ChangeUserPassword(int userId,ChangePasswordDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                return new Response<bool> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "New password and confirm password are required." };
                    
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return new Response<bool> { StatusCode = (int)HttpStatusCode.BadRequest, Message = "New password and confirm password do not match." };
            }

            if (ChangePassword(userId, model.NewPassword))
            {
                return new Response<bool> {Succeeded=true, StatusCode = (int)HttpStatusCode.OK, Message = "Password changed successfully." };
            }
            else
            {
                return new Response<bool> { StatusCode = (int)HttpStatusCode.NotFound, Message = "User not found." };
            }
        }



        #region private methods
        private async Task SendOTPviaSMS(string phoneNumber, string otp)
        {
            // Initialize Twilio client
            
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            // Send OTP via SMS
            var message = await MessageResource.CreateAsync(
                body: $"Your OTP is: {otp}",
                from: new PhoneNumber(_twilioSettings.SenderPhoneNumber),
                to: new PhoneNumber(phoneNumber)
            );
          
            Console.WriteLine(message.Sid);
        }
        private bool ChangePassword(int userId, string newPassword)
        {
            var user = _dbContext.users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Password = EncryptPassword(newPassword); // Assuming you have a method to hash passwords
                _dbContext.SaveChanges();
                return true;
            }
            return false;
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

        private string EncryptPassword(string password)
        {
            //byte[] bytesToEncodePass = Encoding.UTF8.GetBytes(password);
            //var encryptedPassword = Convert.ToBase64String(bytesToEncodePass);
            // Encrypt password using SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Function to decrypt a SHA256 hashed password
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            //byte[] bytesToEncodePass = Encoding.UTF8.GetBytes(inputPassword);
            //var encryptedPassword = Convert.ToBase64String(bytesToEncodePass);
            // Hash the input password using the same method as the encryption
            string hashedInputPassword = EncryptPassword(inputPassword);

            // Compare the hashed input password with the stored hashed password
            // If they match, the passwords are the same
            return hashedInputPassword == hashedPassword;
        }


        private string GenerateOTP()
        {
            Random _random = new Random();

            const int otpLength = 6;
            string otp = string.Empty;

            for (int i = 0; i < otpLength; i++)
            {
                otp += _random.Next(0, 9).ToString();
            }

            return otp;
        }
    
        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.UserType),
                                                     // Add more claims as needed
                }),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
      
        #endregion



    }
}
