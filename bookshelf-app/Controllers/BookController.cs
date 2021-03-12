using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.DTO.Book.Books;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [FormatFilter]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<User> _userManager;

        public BookController(BookRepository repository, IMapper mapper, LinkGenerator linkGenerator, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
        }
        
        //for not logged/registered users
        [HttpGet("all/{page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> Get(int page)
        { 
            try
            {
                var results = await _repository.GetAll(page);
                return _mapper.Map<BookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        //for not logged/registered users
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO>> Get(Guid id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null) return NotFound();
                return _mapper.Map<BookDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        //for logged user
        [HttpGet("logged/genre={genre}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO[]>> GetBooks(string genre)
        { 
            try
            {
                var results = await _repository.GetAllLoogged(genre);
                BookLoggedDTO[] models = _mapper.Map<BookLoggedDTO[]>(results);
                return Ok(models);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        //for logged user
        [HttpGet("logged/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO>> GetBook(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdLogged(id);
                if (result == null) return NotFound();
                return _mapper.Map<BookLoggedDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        //for logged user
        [HttpGet("logged/UserBooks/{genre}/{page}")]
        [Produces("application/json")]
        //change to dto
        public async Task<ActionResult<UserBook[]>> GetUserBooks(string genre, int page)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                var result = await _repository.GetAllUserBooksByGenre(user.Id, genre, page);
                if (result == null) return NotFound();
               // return _mapper.Map<UserBookDTO[]>(result);
               return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        //for logged user
        [HttpGet("logged/UserBooks")]
        [Produces("application/json")]
        //change to dto
        public async Task<ActionResult<UserBook[]>> GetUserBooksForAllGenres()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                var result = await _repository.GetAllUserBooksForAllGenres(user.Id);
                if (result == null) return NotFound();
                // return _mapper.Map<UserBookDTO[]>(result);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        [HttpGet("{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> Get(string genre, int page)
        { 
            try
            {
                var results = await _repository.GetPartByGenre(page, genre);
                return _mapper.Map<BookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        //paging books by genre for logged user
        [HttpGet("logged/{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO[]>> GetLoggedPartByGenre(int page, string genre)
        { 
            try
            {
                var results = await _repository.GetPartByGenre(page, genre);
                return _mapper.Map<BookLoggedDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        
        //change to dto
        [HttpGet("search={search}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> GetBySearch(string search, int page)
        { 
            try
            {
                var results = await _repository.GetBySearch(search, page);
                return _mapper.Map<BookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        /*[HttpPut("logged/UserBooks/add")]
        [HttpPut("{id:int")]
        //to check is UserBookDTO valid for this method
        public async Task<ActionResult<UserBookDTO>> Put(UserBookDTO userBookDto)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null) return NotFound("Couldn't find the talk");

                if (model.Speaker != null)
                {
                    var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null)
                    {
                        talk.Speaker = speaker;
                    }
                }

                _mapper.Map(model, talk);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<TalkModel>(talk);
                }
                else
                {
                    return BadRequest("Failed to update data base");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get talk");
            }
        }*/
    }
}