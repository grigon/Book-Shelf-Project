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



        public async Task MotherSeeder()
        {
            await SeedAsync();
            await SeedUser();
            await SeederGenre();
            await SeederAuthor();
            

        }


        private async Task SeedAsync()
        {
            if (!_context.Roles.Any())
            {
                //Seeder for admin
                var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = _serviceProvider.GetRequiredService<UserManager<User>>();

                IdentityResult roleResult;
                //Adding Admin Role
                ////////////////refactor   TO Json  
                ///// SAVE THE JSONS
                var roleCheck = await RoleManager.RoleExistsAsync("Admin");
                if (!roleCheck)
                {
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
            }

        }

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


        private async Task SeedUser()
        {
            if (!(_context.Users.Count() > 1))
            {


                var filepath = Path.Combine("../bookshelf/Extensions/Jsons/users.json");
                var json = File.ReadAllText(filepath);
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
                var userPass = JsonConvert.DeserializeObject<IEnumerable<SeederHelper>>(json);

                for (int i = 0; i < users.Count(); i++)
                {
                    var user = new User()
                    {
                        UserName = users.ElementAt(i).UserName,
                        Email = users.ElementAt(i).Email,
                        City = users.ElementAt(i).City,
                        RegistrationDate = users.ElementAt(i).RegistrationDate,
                        PhotoPath = users.ElementAt(i).PhotoPath
                    };
                    var cos = userPass.ElementAt(i).Password;

                    var result = await _userManager.CreateAsync(user, userPass.ElementAt(i).Password);
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not create new user in seeder");
                    }
                }
            }
        }

        private async Task SeederGenre()
        {
            if (!_context.Genres.Any())
            {
                var collectionGenres = new List<Genre>();
                var filePath = Path.Combine("../bookShelf/Extensions/Jsons/Genres.json");   // magic string to refactor 
                var json = File.ReadAllText(filePath);
                var genres = JsonConvert.DeserializeObject<IEnumerable<Genre>>(json);

                for (int i = 0; i < genres.Count(); i++)
                {
                    var genre = new Genre()
                    {
                        Name = genres.ElementAt(i).Name
                    };

                    collectionGenres.Add(genre);
                }

                _context.Genres.AddRange(collectionGenres);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeederAuthor()  
        {
            if (!_context.Authors.Any())
            {
                var collecionAuthor = new List<Author>();
                var filePath = Path.Combine("../bookShelf/Extensions/Jsons/Authors.json");  // magic string to refactor  
                var json = File.ReadAllText(filePath);
                var authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(json);

                for (int i = 0; i < authors.Count(); i++)
                {
                    var autor = new Author()
                    {
                        FirstName = authors.ElementAt(i).FirstName,
                        LastName = authors.ElementAt(i).LastName,
                    };

                    collecionAuthor.Add(autor);
                }

                _context.Authors.AddRange(collecionAuthor);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeederBook()
        {
            var collection = new List<Book>();
            var filePath = Path.Combine("../bookShelf/Extensions/Jsons/Books");
            var json = File.ReadAllText(filePath);

            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(json);
            var bookHelper = JsonConvert.DeserializeObject<IEnumerable<SeederHelper>>(json);

            for (int i = 0; i < books.Count(); i++)
            {
                var book = new Book()
                {
                    Title = books.ElementAt(i).Title,
                    Author = await GetAuthor(bookHelper.ElementAt(i).AuthorName),
                    Genre = await GetGenre(bookHelper.ElementAt(i).)
                    Rating = books.ElementAt(i).Rating
                }
            }
        }

        private async Task<Author> GetAuthor(string Name.)
        {
            IQueryable<Author> query = _context.Authors.Where(a => a.FirstName.ToUpper() == Name.ToUpper());

            return await query.FirstOrDefaultAsync();
        }


        private async Task<Genre> GetGenre(string Name)
        {
            IQueryable<Genre> query = _context.Genres.Where(g => g.Name.ToUpper() == Name.ToUpper());

            return await query.FirstOrDefaultAsync();
        }


        private async Task<User> GetUSer(string Mail)
        {
            IQueryable<User> query = _context.Users.Where(e => e.Email.ToUpper() == Mail.ToUpper());

            return await query.FirstOrDefaultAsync();
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