using CloudCustomers.API.Models;
using System.IO;
using System.Reflection.Emit;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Id = 1,
                Name = "Felipe",
                Address = new Address()
                {
                    Street = "Rua 1",
                    City = "Ouro Preto",
                    ZipCode = "12345"
                },
                Email = "felipe@example.com"
            },
            new User
            {
                Id = 2,
                Name = "Maycon",
                Address = new Address()
                {
                    Street = "Rua 1",
                    City = "Ipatinga",
                    ZipCode = "22345"
                },
                Email = "maycon@example.com"
            },
            new User
            {
                Id = 3,
                    Name = "Valeria",
                    Address = new Address()
                    {
                        Street = "Rua 3",
                        City = "Monlevade",
                        ZipCode = "32345"
                    },
                    Email = "valeria@example.com"
            }
        };
    }
}
