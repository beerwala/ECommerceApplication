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
    }
}
