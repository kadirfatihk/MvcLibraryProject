using System.ComponentModel.DataAnnotations;

namespace MvcLibraryProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Posta adresinizi giriniz!")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi giriniz!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Şifrenizi giriniz!")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
