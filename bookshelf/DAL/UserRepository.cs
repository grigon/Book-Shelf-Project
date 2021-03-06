using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RTools_NTS.Util;

namespace bookshelf.DAL
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly ILogger _logger;
        private readonly BaseDbContext _context;

        public UserRepository(BaseDbContext context, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public Task<User[]> GetAll()
        {
            _logger.LogInformation($"Getting all Users");

            IQueryable<User> query = _context.Users;

            return query.ToArrayAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            _logger.LogInformation($"Getting a Camp for {id}");

            IQueryable<User> query = _context.Users;

            return await query.FirstOrDefaultAsync();
        }

        public void Add(User user) 
        {
            _logger.LogInformation("Adding an object of type User to the context.");
            _context.Add(user);
        }

        public void Add<User>(User user) where User : class
        {
            // _logger.LogInformation($"Adding an object of type user to the context.");
            _context.Add(user);
        }

        public Task<User> Update(User t)
        {
            throw new NotImplementedException();
        }

        public void Remove(User user)
        {
            // _logger.LogInformation($"Removing an object of type user to the context.");
            _context.Remove(user);
        }

        public async Task<bool> Commit()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            //TODO return some bool without try block
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IdentityUserToken<string>> GetRefreshTokenById(string id)
        {
            return _context.UserTokens.First(t => t.UserId == id);
        }
        
    }
}