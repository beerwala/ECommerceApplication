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
        Task UserRegistration(RegisterUserDTO user);
        Task LoginUser(LoginDTO login);
        Task ForgotPassword();
    }
}
