using ContactLibrary.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactLibrary.API.DbContexts
{
    public class ContactLibraryContext : DbContext
    {
        public ContactLibraryContext(DbContextOptions<ContactLibraryContext> options)
           : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Contact>().HasData(
                new Contact()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Berry",
                    LastName = "Griffin Beak Eldritch",
                    DateOfBirth = new DateTime(1650, 7, 23),
                    Email = "berry@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Nancy",
                    LastName = "Swashbuckler Rye",
                    DateOfBirth = new DateTime(1668, 5, 21),
                    Email = "nancy@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Eli",
                    LastName = "Ivory Bones Sweet",
                    DateOfBirth = new DateTime(1701, 12, 16),
                    Email = "eli@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Arnold",
                    LastName = "The Unseen Stafford",
                    DateOfBirth = new DateTime(1702, 3, 6),
                    Email = "arnold@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    FirstName = "Seabury",
                    LastName = "Toxic Reyson",
                    DateOfBirth = new DateTime(1690, 11, 23),
                    Email = "seabury@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                    FirstName = "Rutherford",
                    LastName = "Fearless Cloven",
                    DateOfBirth = new DateTime(1723, 4, 5),
                    Email = "rutherford@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                },
                new Contact()
                {
                    Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                    FirstName = "Atherton",
                    LastName = "Crow Ridley",
                    DateOfBirth = new DateTime(1721, 10, 11),
                    Email = "atherton@gmail.com",
                    PhoneNumber = "9876543210",
                    Status = StatusCode.Active
                }
                );



            base.OnModelCreating(modelBuilder);
        }
    }
}
