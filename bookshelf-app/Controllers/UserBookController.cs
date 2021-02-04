using System;
using System.Threading.Tasks;
using bookshelf.DAL;
using bookshelf.Model.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookshelf_app.Controllers
{
    [ApiController]
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
        public IActionResult Get()
        {
            return Ok(_data.GetAll());
        }

        [HttpPut("{Id}")]
        public  Task<ActionResult<UserBook>> UpdateUserBook(Guid id)
        {
            //kontroler musi odebrac wiadomosć od dbcontext
            //rzutowanie w parametrach funkcji obiekt który chcę przekazać do funkcji 
            //UserBook user = new UserBook();
            UserBook user = _data.GetById(id);
            user.Borrowed = false;
            _data.Update(user);
            //await _data.Update(user);


            return null;
            //return NoContent();

        }


    }
}