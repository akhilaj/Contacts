using Xunit;
using FluentAssertions;
using ContactLibrary.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ContactLibrary.API.Services;
using ContactLibrary.API.Entities;
using AutoMapper;
using ContactLibrary.API.Profiles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactLibrary.API.Controllers.Tests
{
    public class ContactsLibraryEvaluatorShould
    {
        [Fact()]
        public void GetContactsTest()
        {
            // create mock version
            var mockRepository = new Mock<IContactLibraryRepository>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ContactsProfile()));
            IMapper mapper = new Mapper(configuration);

            // set up mock version's method
            mockRepository.Setup(x => x.GetContactsAsync())
                          .Returns(Task.FromResult((new List<Contact>(){ new Contact()
                          {
                              Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                              FirstName = "Berry",
                              LastName = "Griffin Beak Eldritch",
                              DateOfBirth = new DateTime(1650, 7, 23),
                              Email = "berry@gmail.com",
                              PhoneNumber = "9876543210",
                              Status = StatusCode.Active
                          } }) as IEnumerable<Contact>));


            var controller = new ContactsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetContacts(new ResourceParameters.ContactsResourceParameters() { SearchQuery = null });

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var contacts = okResult.Value.Should().BeAssignableTo<IEnumerable<Contact>>().Subject;

            contacts.As<List<Contact>>().Count.Should().Be(1);


        }

        [Fact()]
        public void GetContactsTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}