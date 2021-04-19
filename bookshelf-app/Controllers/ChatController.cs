using bookshelf.DAL;
using bookshelf.Model.Chats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [FormatFilter]
    [Route("/Chat")]
    public class ChatController : ControllerBase
    {
        // private readonly ILogger<ChatController> _logger;
        // private readonly ChatRepository _data;
        //
        // public ChatController(ILogger<ChatController> logger, ChatRepository data)
        // {
        //     _logger = logger;
        //     _data = data;
        // }
        //
        // [HttpGet]
        // [Produces("application/json")]
        // public IActionResult Get()
        // {
        //     // return Ok(_data.GetAll());
        // }
    }
}