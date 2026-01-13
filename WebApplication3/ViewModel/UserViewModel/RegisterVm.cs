using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel.UserViewModel
{
    public class RegisterVm
    {
        [Required, MaxLength(256), MinLength(3)]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), MinLength(3), DataType(DataType.Password)]   
        public string Password { get; set; }
        [Required, MaxLength(256), MinLength(3), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
