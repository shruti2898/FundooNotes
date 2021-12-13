using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        // [RegularExpression("",ErrorMessage = "Email is not valid. Please enter a valid email address.")
        public string Email { get; set; }
        [Required]
        public int Password { get; set; }
    }
}
