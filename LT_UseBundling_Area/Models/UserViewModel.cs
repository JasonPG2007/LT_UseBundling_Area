using System.ComponentModel.DataAnnotations;

namespace LT_UseBundling_Area.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please input your user name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
