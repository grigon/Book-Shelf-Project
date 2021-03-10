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

namespace bookshelf
{
    public class DataSeeder
    {
        private readonly BaseDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IServiceProvider _serviceProvider;

        public DataSeeder(BaseDbContext _context, UserManager<User> _userManager, IServiceProvider serviceProvider)
        {
            this._context = _context;
            this._userManager = _userManager;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<User>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            User user = await UserManager.FindByNameAsync("Admin");
            if (user == null)
            {
                user = new User()
                {
                    UserName = "Admin",
                    Email = "admin@email.com",
                };

                var result = await _userManager.CreateAsync(user, "Pass#$wo345d");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }

                await _userManager.AddToRoleAsync(user, "Admin");
            }

            // _context.Database.EnsureCreated();
            // if (!_context.Books.Any())
            // {
            //     var filepath = Path.Combine("../bookshelf/users.json");
            //     var json = File.ReadAllText(filepath);
            //     var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            //     _context.Users.AddRange(users);
            //     _context.SaveChanges(); 
        }
    }
}