using System;
using LibraryApi.Models.Enumerators;
using System.Collections.Generic;
using LibraryApi.Models.Interfaces;

namespace LibraryApi.Models.Entities
{
    public class Author : ITracking, ISoftDelete
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Book> Books { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
