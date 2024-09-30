using Microsoft.AspNetCore.Mvc;
using MvcLibraryProject.Models;
using MvcLibraryProject.ViewModels;

namespace MvcLibraryProject.Controllers
{
    public class AuthorController : Controller
    {
        // Defining a static list of authors. This list will be used throughout the application.
        static List<Author> _authors = new List<Author>()
        {
            new Author{ıd=1, firstName="Fatih", lastName="Köse", dateOfBirth=new DateTime(2001,12,12)},
            new Author{ıd=2, firstName="Mustafa Kemal", lastName="Atatürk", dateOfBirth=new DateTime(1881,05,19)},
        };

        // Action method for displaying the author list.
        public IActionResult AuthorList()
        {
            // Using LINQ to filter for authors that haven't been deleted and converting to AuthorListViewModel.
            var viewModel = _authors
                            .Where(x => x.isDeleted == false)
                            .Select(author => new AuthorListViewModel
                            {
                                ıd = author.ıd,
                                firstName = author.firstName,
                                lastName = author.lastName
                            }).ToList();

            // Sending the created viewModel to the View.
            return View(viewModel);
        }

        // Action method for displaying author details.
        public IActionResult AuthorDetails(int id)
        {
            // Finding the author that matches the incoming id.
            var author = _authors.FirstOrDefault(x => x.ıd == id);

            // If the author is not found, return NotFound.
            if (author == null)
            {
                return NotFound();
            }

            // Creating the viewModel that contains the author details.
            var viewModel = new AuthorDetailsViewModel
            {
                ıd = author.ıd,
                firstName = author.firstName,
                lastName = author.lastName,
                dateOfBirth = author.dateOfBirth
            };

            // Sending the created viewModel to the View.
            return View(viewModel);
        }

        // Action method that handles the GET request for adding a new author.
        [HttpGet]
        public IActionResult AuthorCreate()
        {
            return View();
        }

        // Action method that handles the POST request for adding a new author.
        [HttpPost]
        public IActionResult AuthorCreate(AuthorCreateViewModel formData)
        {
            // If ModelState is not valid (if there are validation errors) return the View.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Finding the largest id in the author list.
            int maxId = _authors.Any() ? _authors.Max(a => a.ıd) : 0;

            // Creating the new author object.
            var newAuthor = new Author
            {
                ıd = maxId + 1,
                firstName = formData.firstName,
                lastName = formData.lastName,
                dateOfBirth = formData.dateOfBirth
            };

            // Adding the new author to the author list.
            _authors.Add(newAuthor);

            // Sending the author list to the View using ViewBag (seems unnecessary).
            ViewBag.Authors = _authors;

            return RedirectToAction("Index", "Home");
        }

        // Action method that handles the GET request for displaying the author edit page.
        [HttpGet]
        public IActionResult AuthorEdit(int id)
        {
            // Finding the author that matches the incoming id.
            var author = _authors.Find(x => x.ıd == id);

            // Creating the viewModel containing the author information.
            var viewModel = new AuthorEditViewModel()
            {
                ıd = author.ıd,
                firstName = author.firstName,
                lastName = author.lastName,
                dateOfBirth = author.dateOfBirth
            };

            // Sending the author list to the View using ViewBag (seems unnecessary).
            ViewBag.Authors = _authors;

            // Returning the View.
            return View(viewModel);
        }

        // Action method that handles the POST request for performing the author editing process.
        [HttpPost]
        public IActionResult AuthorEdit(AuthorEditViewModel formData)
        {
            // If ModelState is not valid (if there are validation errors) return the View.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Finding the author that matches the incoming id.
            var author = _authors.Find(x => x.ıd == formData.ıd);
            
            // Updating the author information.
            author.ıd = formData.ıd; // This line is not necessary, the id will remain the same.
            author.firstName = formData.firstName;
            author.lastName = formData.lastName;
            author.dateOfBirth = formData.dateOfBirth;

            return RedirectToAction("AuthorList", "Author");
        }

        // Action method that handles the GET request for performing the author deletion process.
        public IActionResult AuthorDelete(int id)
        {
            // Finding the author that matches the incoming id.
            var authors = _authors.Find(x => x.ıd == id);

            // Marking the author as SOFT DELETED. This means it will remain in the database but will not be displayed in the list.
            authors.isDeleted = true;

            return RedirectToAction("Index", "Home");
        }
    }
}