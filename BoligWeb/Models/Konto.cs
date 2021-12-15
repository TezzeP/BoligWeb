using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BoligWebApi.Models
{
    public class Konto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name= "Confirm Password")]
        [Compare("Password",
            ErrorMessage = "Dine koder stemer ikke over ens prøv igen")]
        public string ConfirmPassword { get; set; }
        

    }
}
