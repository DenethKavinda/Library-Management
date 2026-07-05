using System;
using System.ComponentModel.DataAnnotations; // 💡 Required for the [Key] attribute
using Microsoft.EntityFrameworkCore;

namespace SarasaviLibrary.Data
{
    // Define the entity model matching the database table
    public class Book
    {
        [Key] // 💡 Explicitly tells Entity Framework that BookNumber is the Primary Key
        public string BookNumber { get; set; } = string.Empty;

        public string Classification { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CopyCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    // 💡 NEW USER ENTITY MODEL
    public class User
    {
        [Key]
        public string UserNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string NIC { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    // Set up the Entity Framework database context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } // 💡 Registers the Users table setup
    }
}