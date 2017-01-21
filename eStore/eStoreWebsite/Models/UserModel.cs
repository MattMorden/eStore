using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Added for task
using System.Threading.Tasks;

//added for Service and DTO classes
using EStoreDAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.Owin.Security;


namespace eStoreWebsite.Models
{
    public class UserModel
    {

        private UserService _dal;
        public string UserID { get; set; }

        [Required(ErrorMessage="Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat Password is required."), CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Age is required."), Range(18, 99, ErrorMessage="Age must be between 18 and 99.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required."), 
        RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email format invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "City is required.")]        
        public string City { get; set; }

        [Required(ErrorMessage = "Mailcode is required."),
        RegularExpression(@"[a-zA-z]\d[a-zA-Z]-\d[a-zA-Z]\d", ErrorMessage = "Enter a proper Postal Code")]
        public string Mailcode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Credit Card is required.")]
        public string CreditcardType { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }

        public string Message { get; set; }
        public bool RememberMe { get; set; }


        /// <summary>
        /// constructor should pass new context
        /// <summary>
        public UserModel()
        {
            _dal = new UserService();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <returns>dictionary containing a status and userid</returns>
        public async Task<string> Register()
        {
            ApplicationUser user = new ApplicationUser();
            // additional customized fields added to std AspNetUsers Table
            user.UserName = UserName;
            user.Firstname = Firstname;
            user.Lastname = Lastname;
            user.Age = Age;
            user.Address1 = Address1;
            user.City = City;
            user.Mailcode = Mailcode;
            user.Region = Region;
            user.Email = Email;
            user.Country = Country;
            user.CreditcardType = CreditcardType;
            return await _dal.CreateAsync(user, Password);
        }


        public async Task<bool> LoginAsync(IAuthenticationManager authMgr)
        {
            bool loginOK = false;
            ApplicationUser user = await _dal.RetrieveByNameAsync(UserName, Password, authMgr);

            if (user != null)
            {
                Firstname = user.Firstname;
                Lastname = user.Lastname;
                UserName = user.UserName;
                Email = user.Email;
                Age = Convert.ToInt32(user.Age);
                Address1 = user.Address1;
                City = user.City;
                Mailcode = user.Mailcode;
                Region = user.Region;
                Country = user.Country;
                CreditcardType = user.CreditcardType;
                UserID = user.Id;
                loginOK = true;
            }
            return loginOK;
        }//end LoginAsync


    }
}