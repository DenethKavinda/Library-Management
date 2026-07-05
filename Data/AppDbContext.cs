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

    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public string ReservationCode { get; set; } = string.Empty;
        public string UserNumber { get; set; } = string.Empty;
        public string BookNumber { get; set; } = string.Empty;
        public DateTime ReservedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // 💡 Added Status tracking
    }

    public class Loan
    {
        [Key]
        public string LoanId { get; set; } = string.Empty;
        public string ReservationCode { get; set; } = string.Empty;
        public string UserNumber { get; set; } = string.Empty;
        public string BookNumber { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Received
    }

    public class Return
    {
        [Key]
        public string ReturnCode { get; set; } = string.Empty;
        public string LoanId { get; set; } = string.Empty;
        public DateTime ReturnedDate { get; set; } = DateTime.Now;
    }

    public class Inquiry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserNumber { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string NIC { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Reviewed, Resolved
    }

    // Set up the Entity Framework database context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } // 💡 Registers the Users table setup
        public DbSet<Reservation> Reservations { get; set; } // 💡 Registered table map
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
    }
}