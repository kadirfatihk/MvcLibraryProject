﻿namespace MvcLibraryProject.Models
{
    public class User
    {
        // Represents an user in the library system.
        public int ıd { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public DateTime joinDate { get; set; }
    }
}
