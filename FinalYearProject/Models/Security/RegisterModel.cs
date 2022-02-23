using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models.Security
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }

    }
}
