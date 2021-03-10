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
using Microsoft.AspNetCore.JsonPatch;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [FormatFilter]
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
        //public async Task<ActionResult<ChatMessageReadDTO[]>> GetAllMesegges()
        //{
        //    //try
        //    //{
        //    //    var result = await _chatRepository.GetAll();

        //    //    return _mapper.Map<ChatMessageReadDTO[]>(result);
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    _logger.LogError("An error has occured with chat repository");
        //    //    return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        //    //}

        //}

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
                if (message.Message.Length < 1) return BadRequest("Empty object");

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

        [HttpPatch("{chatid}/{messageId}")]
        public async Task<ActionResult<ChatMessageUpdateDTO>> UpdateMessageForActualChat(Guid messageId, JsonPatchDocument<ChatMessageUpdateDTO> messageUpdate)
        {
            try
            {
                var id = new Guid("47435eee-7ead-484a-beb2-3cbf5b768b67");// identity

                if (messageUpdate == null) return NotFound();

                var messageFromDB = await _chatRepository.GetMessageById(id, messageId);

                if (messageFromDB == null)
                {
                    return NotFound();
                }
                else
                {
                    var messagePath = _mapper.Map<ChatMessageUpdateDTO>(messageFromDB);
                    messageUpdate.ApplyTo(messagePath, ModelState);
                    _mapper.Map(messagePath, messageFromDB);
                    if (await _chatRepository.SaveChanges())
                    {
                        var url = _linkGenerator.GetPathByAction(HttpContext, "ActualUserChat",
                            values: new { chatid = messageFromDB.Chat.ChatId });

                        return Created(url, _mapper.Map<ChatMessageUpdateDTO>(messagePath));
                    }
                    else
                    {
                        return BadRequest("Failed save edited chat message to database in chat repository");
                    }
                }
            }
            catch (Exception)
            {
                _logger.LogError("Update chat message came across a problem, edit message failed");
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
            //logger???
        }

        [HttpDelete("{chatid}/{messageId}")]
        public async Task <IActionResult> DeleteMessageFromChat(Guid messageId)
        {
            try
            {
                var id = new Guid("47435eee-7ead-484a-beb2-3cbf5b768b67");

                var messagetoDelete = await _chatRepository.GetMessageById(id, messageId);

                if (messagetoDelete == null) return NotFound();

                _chatRepository.Delete(messagetoDelete);

                if (await _chatRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                _logger.LogError("An error has occured whit chat repository, couldn't delete message in this chat");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();
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
                // link user , with other users in this same chat 
                return userchat;

            }
            catch (Exception)
            {
                _logger.LogError("An error has occured with chat repository");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost("newchat")]
        public async Task<ActionResult<ChatUserCreateDTO>> AddChat(ChatUserCreateDTO secondUser)
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
                firstUserChat.User = firstParticipant;

                _chatRepository.Create(firstUserChat);
                if (!await _chatRepository.SaveChanges())
                {
                    _logger.LogError("An error  occured with save user chat to database in chat repository");
                    return BadRequest($"failed save {firstUserChat.GetType()} for first user to database");
                }

                var secondParticipant = await _chatRepository.GetUserById(secondUser.userId);

                var secondUserChat = _mapper.Map<ChatUser>(secondUser);

                secondUserChat.Chat = firstUserChat.Chat;
                secondUserChat.User = secondParticipant;

                _chatRepository.Create(secondUserChat);

                if (await _chatRepository.SaveChanges())
                {

                    var url = _linkGenerator.GetPathByAction(HttpContext, "ActualUserChat",
                        values: new { chatid = newChatId.ChatId });

                    return Created(url, _mapper.Map<ChatUserReadDTO>(secondUserChat));//po co zwracać 
                }
                else
                {
                    return BadRequest("Failed save second user to chat");
                }
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