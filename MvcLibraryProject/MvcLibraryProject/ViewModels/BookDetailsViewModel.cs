namespace MvcLibraryProject.ViewModels
{
    public class BookDetailsViewModel
    {
        public int ıd { get; set; }
        public string title { get; set; }
        public int authorId { get; set; }
        public string genre { get; set; }
        public DateTime publishDate { get; set; }
        public string ısbn { get; set; }
        public int copiesAvailable { get; set; }
        public bool isDeleted { get; set; }

        // Author details associated with the book.
        public string authorFirstName { get; set; }
        public string authorLastName { get; set; }
    }
}
