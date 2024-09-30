using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcLibraryProject.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "E-Posta adresinizi giriniz!")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-posta adresi giriniz!")]
        [Remote(action: "VerifyEmail", controller: "Auth", ErrorMessage = "Bu E-Posta adresi kullanımda!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Şifrenizi giriniz!")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakterden oluşmalıdır!")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarını giriniz!")]
        [DataType(DataType.Password)]
        [Compare(nameof(password), ErrorMessage = "Şifre eşleşmiyor!")]
        public string passwordConfirm { get; set; }

        [Required(ErrorMessage = "İsminizi giriniz!")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Telefon numaranızı giriniz!")]
        public string phoneNumber { get; set; }
    }
}
