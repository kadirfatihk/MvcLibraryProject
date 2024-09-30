﻿using System.ComponentModel.DataAnnotations;

namespace MvcLibraryProject.ViewModels
{
    public class AuthorCreateViewModel
    {
        [Required(ErrorMessage = "Yazar ismi giriniz!")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Yazar soyismi giriniz!")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Yazarın doğum tarihini giriniz!")]
        public DateTime dateOfBirth { get; set; }
    }
}
