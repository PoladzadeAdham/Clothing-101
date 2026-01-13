using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel.UserViewModel
{
    public class LoginVm
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
