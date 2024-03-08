using Application.Interface.Repository;
using Application.Wrappper;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserManagement.Command
{
    public class RegistrationCommand:IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public DateTime DOB { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string ProfileImage { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
    }
    //public class CreateUserHandler:IRequestHandler<RegistrationCommand, Response<int>>
    //{
    //    private readonly IUserRepository _userRepository;
    //    public CreateUserHandler(IUserRepository userRepository)
    //    {
    //        _userRepository= userRepository;
    //    }

       
    //}

}
