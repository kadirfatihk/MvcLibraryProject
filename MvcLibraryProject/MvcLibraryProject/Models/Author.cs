namespace MvcLibraryProject.Models
{
    // Represents an author in the library system.
    public class Author
    {
        public int ıd { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isDeleted { get; set; }
    }
}
