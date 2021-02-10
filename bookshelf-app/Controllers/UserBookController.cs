using System;
using System.Threading.Tasks;
using bookshelf.DAL;
using bookshelf.Model.Books;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [FormatFilter]
    
    [Route("/UserBooks")]
    public class UserBookController : ControllerBase
    {
        private readonly ILogger<UserBookController> _logger;
        private readonly IBaseRepository<UserBook> _data;

        public UserBookController(ILogger<UserBookController> logger, IBaseRepository<UserBook> data)
        {
            _logger = logger;
            _data = data;
        }

        [HttpGet]
        
        [Produces("application/json")]
        //public async Task<ActionResult<IBaseRepository<UserBook>>> Get()
        //{
        //    return await Ok(_data.GetAll());
        //}
        public  IActionResult Get()
        {
            return Ok(_data.GetAll());
        }

        [HttpPut("UpdateBorrowedStatus/{id}")]
        public async Task<ActionResult<UserBook>> UpdateUserBook(Guid id)
        {
            UserBook user = _data.GetById(id);
            _data.Update(user);
            _data.Commit();
            return user;
        }
        
        [HttpPut("UpdateisPublicStatus/{id}")]
        public async Task<ActionResult<UserBook>> UpdateUserBookIsPublic(Guid id)
        {
            UserBook user = _data.GetById(id);
            _data.UpdateIsPublic(user);
            _data.Commit();
            return user;
        }
    }
}