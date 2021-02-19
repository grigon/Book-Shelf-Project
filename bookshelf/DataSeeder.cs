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
using Newtonsoft.Json;

namespace bookshelf
{
    public class DataSeeder
    {
        private readonly BaseDbContext _context;
        private readonly UserManager<User > _userManager;

        public DataSeeder(BaseDbContext _context, UserManager<User> _userManager)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public async Task SeedAsync()
        {
            User user = await _userManager.FindByNameAsync("Roman");
            if (user == null) 
            {
                user = new User()
                {
                    UserName = "Roman",
                    Email = "email@email.com",
                };
            
                var result = await _userManager.CreateAsync(user, "Pass#$wo345d");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
            
            _context.Database.EnsureCreated();
            if (!_context.Books.Any())
            {
                var filepath = Path.Combine("../bookshelf/users.json");
                var json = File.ReadAllText(filepath);
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
                _context.Users.AddRange(users);
                _context.SaveChanges(); 
            } 
        }
    }
} 