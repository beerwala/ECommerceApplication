using Application.DTO;
using Application.Interface.Repository;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> ResgisterAsync([FromBody]RegisterUserDTO registerUserDTO)
        {
         
            return Ok( _userRepository.UserRegistration(registerUserDTO));
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO login)
        {

            return Ok(_userRepository.LoginUser(login));
        }

        [HttpPost("AuthenticateLoginUser")]
        public async Task<IActionResult> AuthenticateLogin(string Username,string code)
        {

            return Ok(_userRepository.AuthenticationLoginUser(Username, code));
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPass([FromBody] ForgotPasswordDTO email)
        {

            return Ok(_userRepository.SendResetPasswordOTP(email));
        }

        [HttpPost("ValidateOTP")]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpDTO otpDTO)
        {

            return Ok(_userRepository.ResetPassword(otpDTO));
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> PasswordChange(int userId ,ChangePasswordDTO passDTO)
        {

            return Ok(_userRepository.ChangeUserPassword(userId,passDTO));
        }


    }
}
