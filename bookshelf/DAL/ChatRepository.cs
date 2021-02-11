using System;
using System.Collections;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;

namespace bookshelf.DAL
{
    public class ChatRepository : IBaseRepository<Chat>
    {
        private readonly IBaseContext _context;

        public ChatRepository(IBaseContext context)
        {
            _context = context;
        }

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public Chat GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Chat t)
        {
            throw new NotImplementedException();
        }

        public UserBook Update(Chat t)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public UserBook UpdateIsPublic(Chat t)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}