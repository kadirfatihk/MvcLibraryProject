using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MvcLibraryProject.Models;
using MvcLibraryProject.ViewModels;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace MvcLibraryProject.Controllers
{
    public class AuthController : Controller
    {
        // Static list to store users. In a real application, this would be a database.
        private static List<User> _users = new List<User>()
        {
            new User{ıd=1, email="text@gmail.com", password = "textpassword", fullName="text", phoneNumber="000", joinDate = DateTime.Now},
        };

        // Used for data protection (e.g., encrypting/decrypting passwords).
        private readonly IDataProtector _dataProtector;

        // Constructor to inject the data protection provider.
        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            // Create a protector instance with a specific purpose ("security").
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        // GET action for displaying the sign-up page.
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST action to handle sign-up form submission.
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel formData)
        {
            // Check if the form data is valid based on model validation rules.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Check if a user with the same email already exists (case-insensitive).
            var user = _users.FirstOrDefault(x => x.email.ToLower() == formData.email.ToLower());
            if (user is not null)
            {
                // Add a model error specifically to the "email" field.
                ModelState.AddModelError("email", "E-Posta adresi kullanımda");
                return View(formData);
            }

            // Create a new user object.
            var newUser = new User()
            {
                ıd = _users.Max(x => x.ıd) + 1,
                email = formData.email.ToLower(),
                // Encrypt the password before storing it.
                password = _dataProtector.Protect(formData.password),
                fullName = formData.fullName,
                phoneNumber = formData.phoneNumber,
                joinDate = DateTime.Now
            };

            // Add the new user to the list.
            _users.Add(newUser);

            // Redirect to the login page after successful sign-up.
            return RedirectToAction("Login", "Auth");
        }

        // GET action to display the login page.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST action to handle the login form submission.
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            // Find the user by email (case-insensitive).
            var user = !string.IsNullOrEmpty(formData.email) ?
    _users.FirstOrDefault(x => x.email.ToLower() == formData.email.ToLower()) : null;

            // Check if the user exists.
            if (user == null)
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
                return View(formData);
            }

            // Decrypt the stored password.
            var rawPassword = _dataProtector.Unprotect(user.password);

            // Compare the entered password with the decrypted password.
            if (rawPassword == formData.password)
            {
                // User authenticated successfully:

                // Create a list of claims for the user.
                var claims = new List<Claim>();
                claims.Add(new Claim("email", user.email));
                claims.Add(new Claim("id", user.ıd.ToString()));
                claims.Add(new Claim("fullname", user.fullName));
                claims.Add(new Claim("phonenumber", user.phoneNumber));

                // Create a ClaimsIdentity based on the claims and authentication scheme.
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Configure authentication properties.
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,    // Allow session refresh.
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMinutes(30)) // Set session expiration.
                };

                // Sign in the user asynchronously using cookie authentication.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimIdentity), authProperties);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Şifre yanlış.");
                return View(formData);
            }

            return RedirectToAction("Index", "Home");

        }
    }
}