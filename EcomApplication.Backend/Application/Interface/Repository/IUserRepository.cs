using Application.DTO;
using Application.Wrappper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUserRepository:IGenericRepositoryAsync<ApplicationUser>
    {
        Task<Response<int>> UserRegistration(RegisterUserDTO user);
        Task<Response<string>> LoginUser(LoginDTO login);

        Task<Response<string>> AuthenticationLoginUser(string username,string code);
        Task<Response<string>> SendResetPasswordOTP(ForgotPasswordDTO email);
        Task<Response<string>> ResetPassword(ValidateOtpDTO otpDTO);
        Task<Response<bool>> ChangeUserPassword(int userId, ChangePasswordDTO model);

    }
}
