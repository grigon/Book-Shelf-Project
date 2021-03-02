using bookshelf.DAL;
using bookshelf.Model.Chats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [FormatFilter]
    [Route("/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatRepository _chatRepository;


        public ChatController(ILogger<ChatController> logger, IChatRepository chatRepository)
        {
            _logger = logger;
            this._chatRepository = chatRepository;
            
        }
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<ChatMessage[]>> GetAllMesegges()
        {
            try
            {
                var result = await _chatRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
            


        }
    }
}