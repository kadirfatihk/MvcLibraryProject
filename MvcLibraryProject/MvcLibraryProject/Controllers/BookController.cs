using Microsoft.AspNetCore.Mvc;
using MvcLibraryProject.Models;
using MvcLibraryProject.ViewModels;

namespace MvcLibraryProject.Controllers
{
    public class BookController : Controller
    {
        // Defined the static book list. This list will be used throughout the application.
        static List<Book> _books = new List<Book>()
        {
            new Book{ıd=1, title="Sakarya Gezi Rehberi" ,authorId=1, genre="Seyahat", publishDate= new DateTime(2024,1,1), ısbn="54-00-54", copiesAvailable=54},
            new Book{ıd=2, title="Nutuk", authorId=2, genre="Söylev", publishDate = new DateTime(1927,10,15), ısbn="975-16-0162-2", copiesAvailable=1923}
        };

        // Static author list defined
        static List<Author> _authors = new List<Author>()
        {
            new Author{ıd=1, firstName="Fatih", lastName="Köse", dateOfBirth=new DateTime(2001,12,12)},
            new Author{ıd=2, firstName="Mustafa Kemal", lastName="Atatürk", dateOfBirth=new DateTime(1881,05,19)}
        };

        // Action method used to display the book list.
        public IActionResult BookList()
        {
            //  Combined books and authors with LINQ query and filtered undeleted books
            var viewModel = _books
                            .Where(x => x.isDeleted == false)
                            .Join(
                                   _authors,
                                   book => book.authorId,
                                   author => author.ıd,
                                   (book, author) => new BookListViewModel
                                   {
                                       ıd = book.ıd,
                                       title = book.title,
                                       genre = book.genre,
                                       authorId = book.authorId,
                                       authorName = $"{author.firstName} {author.lastName}"
                                   }).ToList();
            
            // The created viewModel was sent to View.
            return View(viewModel);
        }

        // Action method used to display the book detail
        public IActionResult BookDetails(int id)
        {
            // Found the book matching the incoming id.
            var book = _books.Find(x=>x.ıd == id);

            // If the book is not found, NotFound is returned.
            if (book == null)  
            {
                return NotFound();
            }

            // He found the author of the book.
            var author = _authors.FirstOrDefault(x => x.ıd == book.authorId);

            // Created viewModel with book details.
            var viewModel = new BookDetailsViewModel
            {
                ıd = book.ıd,
                title = book.title,
                genre = book.genre,
                publishDate = book.publishDate,
                ısbn = book.ısbn,
                copiesAvailable = book.copiesAvailable,
                authorFirstName = author?.firstName ?? "Yazar bilgisi yok.",
                authorLastName = author?.lastName ?? ""
            };

            return View(viewModel);
        }

        // Action method that responds to a GET request to add a new book.
        [HttpGet]
        public IActionResult BookCreate()
        {
            // Author list sent to View with ViewBag.
            ViewBag.Authors = _authors; 

            return View();
        }

        // Action method that responds to a POST request to add a new book.
        [HttpPost]
        public IActionResult BookCreate(BookCreateViewModel formData)
        {
            // If the ModelState is not valid (validation errors), we send the author list to the View with ViewBag. Then the View is returned.
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _authors;
                return View(formData);
            }

            // The biggest id on the book list has been found.
            int maxId = _books.Any() ? _books.Max(x => x.ıd) : 0;

            // Created the new book object.
            var newBook = new Book
            {
                ıd = maxId + 1, // Id of the new book will be maxId + 1
                title = formData.title,
                genre = formData.genre,
                publishDate = formData.publishDate,
                ısbn = formData.ısbn,
                copiesAvailable = formData.copiesAvailable,
                authorId = formData.AuthorId
            };

            // The new book has been added to the book list.
            _books.Add(newBook);

            // Redirected to book list page.
            return RedirectToAction("BookList");
        }

        // Action method that satisfies the GET request used to display the book edit page.
        [HttpGet]
        public IActionResult BookEdit(int id)      
        {
            // Found the book matching the incoming id.
            var book = _books.Find(x=>x.ıd == id);

            // Necessary information sent to view
            var viewModel = new BookEditViewModel()
            {
                ıd = book.ıd,
                title = book.title,
                genre = book.genre,
                publishDate = book.publishDate,
                ısbn = book.ısbn,
                copiesAvailable = book.copiesAvailable,
                AuthorId = book.authorId
            };

            // Author list sent to View with ViewBag.
            ViewBag.Authors = _authors;

            return View(viewModel);
        }

        // Action method that responds to the POST request used to perform the book editing operation.
        [HttpPost]
        public IActionResult BookEdit(BookEditViewModel formData)
        {
            if(!ModelState.IsValid)
            {
                return View(formData);
            }

            // Found the book matching the incoming id.
            var book = _books.Find(x => x.ıd == formData.ıd);

            // Book information updated.
            book.title = formData.title;
            book.genre = formData.genre;
            book.publishDate = formData.publishDate;
            book.ısbn = formData.ısbn;
            book.copiesAvailable = formData.copiesAvailable;
            book.authorId = formData.AuthorId;

            return RedirectToAction("BookList", "Book");
        }

        // The Action method that satisfies the GET request used to perform the book deletion.
        [HttpGet]
        public IActionResult BookDelete(int id)
        {
            var book = _books.Find(x=>x.ıd == id);

            // The book is marked as SOFT DELETED. So it will remain in the database but will not appear in the list.
            book.isDeleted = true;

            // Redirected to homepage
            return RedirectToAction("Index", "Home");
        }
    }
}
