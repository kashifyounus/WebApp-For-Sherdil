using System.ComponentModel.DataAnnotations;

namespace WebAppForSherdilCloud.Models
{
    public class Doctor : BaseUser
    {
        [Required(ErrorMessage = "Speciality is required")]
        public int Speciality { get; set; }
    }
}
