using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bookshelf.Extensions;

namespace bookshelf
{
    public class DataSeeder
    {
        private readonly BaseDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DataSeeder(BaseDbContext _context, UserManager<User> userManager, IServiceProvider serviceProvider, IPasswordHasher<User> passwordHasher)
        {
            this._context = _context;
            this._userManager = userManager;
            _serviceProvider = serviceProvider;
            this._passwordHasher = passwordHasher;
        }

        //public async Task SeedAsync()
        //{
        //    var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var UserManager = _serviceProvider.GetRequiredService<UserManager<User>>();

        //    IdentityResult roleResult;
        //    //Adding Admin Role
        //    var roleCheck = await RoleManager.RoleExistsAsync("Admin");
        //    if (!roleCheck)
        //    {
        //        //create the roles and seed them to the database
        //        roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
        //    }

        //    User user = await UserManager.FindByNameAsync("Admin");
        //    if (user == null)
        //    {
        //        user = new User()
        //        {
        //            UserName = "Admin",
        //            Email = "admin@email.com",
        //        };

        //        var result = await _userManager.CreateAsync(user, "Pass#$wo345d");
        //        if (result != IdentityResult.Success)
        //        {
        //            throw new InvalidOperationException("Could not create new user in seeder");
        //        }

        //        await _userManager.AddToRoleAsync(user, "Admin");
        //    }




        // _context.Database.EnsureCreated();
        // if (!_context.Books.Any())
        // {
        //     var filepath = Path.Combine("../bookshelf/users.json");
        //     var json = File.ReadAllText(filepath);
        //     var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
        //     _context.Users.AddRange(users);
        //     _context.SaveChanges(); 
        //}

        //public void Seed()
        //{
        //    if (_context.Database.CanConnect())
        //    {
        //        if (!(_context.Users.Count() > 3)) 
        //        { 
        //        var users = GetUsers();
        //        _context.Users.AddRange(users);
        //        _context.SaveChanges();
        //    }
        //    }
        //}


        public async Task SeedUser()
        {
            //var UserManager = _serviceProvider.GetRequiredService<UserManager<User>>();

            //IdentityResult roleResult;
            ////Adding Admin Role
            //var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            //if (!roleCheck)
            //{
            //    //create the roles and seed them to the database
            //    roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            //}

            //User user = await UserManager.FindByNameAsync("Admin");
            //if (user == null)
            //{
            var filepath = Path.Combine("../bookshelf/users.json");
            var json = File.ReadAllText(filepath);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            var userPass = JsonConvert.DeserializeObject<IEnumerable<PasswordSeederHelper>>(json);

            foreach (var item in userPass)
            {
                Console.WriteLine(item.Password);
            }

            //var user = new User()
            //{
            //    UserName = users
            //    Email = "dominik@wik.name",
            //    City = "Warsaw",
            //    RegistrationDate =  
            //    PhotoPath = "Empty path"
            //};

            //var result = await _userManager.CreateAsync(user, "Magic50+");
            //if (result != IdentityResult.Success)
            //{
            //    throw new InvalidOperationException("Could not create new user in seeder");
            //}

        }


        //public IEnumerable<User> GetUsers()
        //{
        //    var users = new List<User>();

        //    var user1 = new User()
        //    {
        //        UserName = "Dominik",
        //        Email = "dominik@wik.name",
        //        City = "Warsaw",
        //        PhotoPath = "Empty path"

        //    };
        //   var lipa = _passwordHasher.HashPassword(user1, "Magic50+");
        //    user1.PasswordHash = lipa;

        //    users.Add(user1);
        //    //new User()
        //    //    {
        //    //        UserName =  "Mike",
        //    //        Email    = "mike@gmail.com",
        //    //        PasswordHash = "Lipa6000*",
        //    //        City =  "Boston",
        //    //        PhotoPath = "Empty path"

        //    //    },
        //    //};
        //    return users;
        //}
    }
}