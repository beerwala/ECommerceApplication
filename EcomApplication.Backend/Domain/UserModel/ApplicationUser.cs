using Domain.Model.CascadingData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string EncryptedUsername {  get; set; }
        public string EncryptedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required]
        public string UserType { get; set; }
        public DateTime DOB { get; set; }

        [Phone]
        [Required(ErrorMessage ="Mobile Number Required")]
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string ProfileImage { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }


        // Navigation properties
        //public State State { get; set; }
        //public Country Country { get; set; }

    }
}
