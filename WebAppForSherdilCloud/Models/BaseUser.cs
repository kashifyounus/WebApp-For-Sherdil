using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppForSherdilCloud.Models
{
    public class BaseUser : BaseModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Name is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
    }
}
