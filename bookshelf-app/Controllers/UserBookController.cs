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
        public IActionResult Get()
        {
            return Ok(_data.GetAll());
        }
    }
}