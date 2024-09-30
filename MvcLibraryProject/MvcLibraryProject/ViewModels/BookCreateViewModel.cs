using System.ComponentModel.DataAnnotations;

namespace MvcLibraryProject.ViewModels
{
    public class BookCreateViewModel
    {
        [Required(ErrorMessage = "Kitap bilgisini giriniz!")]
        public string title { get; set; }

        [Required(ErrorMessage = "Kitap türünü giriniz!")]
        public string genre { get; set; }

        [Required(ErrorMessage = "Yayın tarihini giriniz!")]
        public DateTime publishDate { get; set; }

        [Required(ErrorMessage = "ISBN numarasını giriniz!")]
        public string ısbn { get; set; }

        [Required(ErrorMessage = "Mevcut kopya sayısını giriniz")]
        public int copiesAvailable { get; set; }

        [Required(ErrorMessage = "Yazar seçiniz!")]
        public int AuthorId { get; set; }
    }
}
