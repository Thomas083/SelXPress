using SelXPressApi.Data;
using SelXPressApi.Models;

namespace SelXPressApi.Helper;

public class Seed
{
    private readonly DataContext _context;

    public Seed(DataContext dataContext)
    {
        _context = dataContext;
    }

    public void SeedDataContext()
    {
        if (!_context.Users.Any() && !_context.Roles.Any())
        {
            Role customerRole = new Role()
            {
                Name = "Customer"
            };
            Role sellerRole = new Role()
            {
                Name = "Seller"
            };
            Role operatorRole = new Role()
            {
                Name = "Operator"
            };

            User userCustomer1 = new User()
            {
                Username = "LeBirz",
                Email = "ugo.bertrand@epitech.eu",
                Password = "password",
                Role = customerRole
            };
            
            User userCustomer2 = new User()
            {
                Username = "Elsharion",
                Email = "david.vacossin@epitech.eu",
                Password = "password",
                Role = customerRole
            };

            User userSeller = new User()
            {
                Username = "Aliak",
                Email = "thomas.debray@epitech.eu",
                Password = "password",
                Role = sellerRole
            };

            User userOperator = new User()
            {
                Username = "Maxence_Leroy",
                Email = "maxence.leroy@epitech.eu",
                Password = "password",
                Role = operatorRole
            };

            User userOperator2 = new User()
            {
                Username = "Mockingame",
                Email = "julien.lamalle@epitech.eu",
                Password = "password",
                Role = operatorRole
            };


            _context.Roles.AddRange(customerRole, sellerRole, operatorRole);
            _context.Users.AddRange(userCustomer1, userCustomer2, userSeller, userOperator, userOperator2);
            _context.SaveChanges();
        }
    }
}