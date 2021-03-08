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
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ChatController(ILogger<ChatController> logger, IChatRepository chatRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _logger = logger;
            this._chatRepository = chatRepository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<ChatMessageReadDTO[]>> GetAllMesegges()
        {
            try
            {
                var result = await _chatRepository.GetAll();
                
                return _mapper.Map<ChatMessageReadDTO[]>(result);
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
        [HttpGet("{chatid}")]//add paginate 
        public async Task<ActionResult<ChatMessageReadDTO[]>> ActualUserChat(Guid chatid)
        {
            try
            {
                var chat = await _chatRepository.GetAllMessagesForChat(chatid);

                if (chat == null) return NotFound();

                return _mapper.Map<ChatMessageReadDTO[]>(chat);
            }
            catch (Exception)
            {

                _logger.LogError($"An error has occuredd with chat repository, get meassages for chat {chatid},  It came across a problem");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            
        }
        [HttpPost("{chatid}")]
        public async Task<ActionResult<ChatMessageCreateDTO>> AddMessageInChat(Guid chatid, ChatMessageCreateDTO message)
        {
            try
            {//dużo w kontrolerze
                var id = new Guid("47435eee-7ead-484a-beb2-3cbf5b768b67");//from identity
                //
                var user = await _chatRepository.GetUserById(id);

                    if (user == null) return BadRequest("user doesn't exist");

                var chat = await _chatRepository.GetChatById(chatid);

                    if (chat == null) return BadRequest("user doesn't exist");
                var messageToDb = _mapper.Map<ChatMessage>(message);

                messageToDb.Chat = chat;
                messageToDb.MessageAuthor = user;

                _chatRepository.Create(messageToDb);
                
                if (await _chatRepository.SaveChanges())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext, "ActualUserChat",
                        values: new { chatid = messageToDb.Chat.ChatId });

                    return Created(url, _mapper.Map<ChatMessageReadDTO>(messageToDb));//po co zwracać
                }
                else
                {
                    return BadRequest("failed save chat message to data base in chat repository");
                }

            }
            catch (Exception)
            {
                _logger.LogError("An error has occured with chat repository, Couldn't save message in database");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }

        [HttpGet("userchat")]
        public async Task<ActionResult<ChatUser[]>> AllUsersChats()
        {
            try
            {
                // this id is from identity....
                // method not complete in progress...
                var id = new Guid("a7ecde05-58ef-4230-87e8-08c9409edf9e");
                var userchat = await _chatRepository.AllChatIdByUserId(id);

                return userchat;

            }
            catch (Exception)
            {
                _logger.LogError("An error has occured with chat repository");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost] 
        public async  Task<ActionResult<ChatUser>> AddChat(ChatUser secondUser)
        {
            try
            {

                //first user to this entity will be from identity , create new object chat and assign to chatuser 
                //first user chat initiator
                var id = new Guid("47435eee-7ead-484a-beb2-3cbf5b768b67");
                var firstParticipant = await _chatRepository.GetUserById(id);

                    if (firstParticipant == null) return BadRequest("user doesn't exist");

                
                var newChatId = new Chat();
                var firstUserChat = new ChatUser();
                firstUserChat.Chat = newChatId;
                firstUserChat.User = firstParticipant = await _chatRepository.GetUserById(id);
                

                _chatRepository.Create(firstUserChat);
                if (! await _chatRepository.SaveChanges())
                {
                    _logger.LogError("An error  occured with save user chat to database in chat repository");
                    return BadRequest($"failed save {firstUserChat.GetType()} for first user to database");
                }
                

                var secondParticipant = await _chatRepository.GetUserById(secondUser.User)

                var SecondUserChat = new ChatUser();



                //second chatuser will come from frot in json.
                //for two 

                //mojje.Chat = chat;

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