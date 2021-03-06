using bookshelf.DAL;
using bookshelf.Model.Chats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using bookshelf.DTOS;
using Microsoft.AspNetCore.Routing;


namespace bookshelf_app.Controllers
{
    [ApiController]
    //[FormatFilter]
    [Route("/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatController(ILogger<ChatController> logger, IChatRepository chatRepository, IMapper mapper)
        {
            _logger = logger;
            this._chatRepository = chatRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<ChatMessageDTO[]>> GetAllMesegges()
        {
            try
            {
                var result = await _chatRepository.GetAll();


                //chat.ChatId = new Guid();
                //var replace = _mapper.Map<Chat>(chat);

                //_chatRepository.Create(replace);
                
                return _mapper.Map<ChatMessageDTO[]>(result);
            }
            catch (Exception)
            {

                _logger.LogError("An error has occured with chat repository");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

        }
        [HttpGet("allchats/admin")]
        public async Task<ActionResult<Chat[]>> AllChatAdmin()
        {
            try
            {
                var chats = await _chatRepository.AllChatsForAdmin();

                return chats;
            }
            catch (Exception)
            {
                _logger.LogError("An error has occured with chat repository load all chats for admin");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            
        }
        [HttpGet("userchat")]
        public async Task<ActionResult<ChatUser[]>> AllUsersChats()
        {
            try
            {
                var userchat = await _chatRepository.GetAllChatsUser();

                return userchat;

            }
            catch (Exception)
            {
                _logger.LogError("An error has occured with chat r");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost] 
        public async  Task<ActionResult<ChatUser>> AddChat(Chat chat2)
        {
            try
            {
                var chat = new Chat();

                //create chatId , retrive id from DB  assign chat to   table userId , user id-logged user
                // second line - chat-id  we have // user id will come from client format json.
                ;


                var mojje = new ChatUser();
                mojje.Chat = chat;

                var id = new Guid("a7ecde05-58ef-4230-87e8-08c9409edf9e");

                var user = await _chatRepository.GetUserById(id);

                mojje.User = user;



                _chatRepository.Create(mojje);
                return mojje;
            }
            catch (Exception)
            {

                _logger.LogError("An error has occured with chat repository");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        //[HttpPost]
        //public async Task<ActionResult>
    }
}