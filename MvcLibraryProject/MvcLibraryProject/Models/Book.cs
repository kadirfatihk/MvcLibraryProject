namespace MvcLibraryProject.Models;

public class Book
{
    // Represents an book in the library system.
    public int ıd { get; set; }
    public string title { get; set; }
    public int authorId { get; set; }
    public string genre { get; set; }
    public DateTime publishDate { get; set; }
    public string ısbn { get; set; }
    public int copiesAvailable { get; set; }
    public bool isDeleted { get; set; }
}
