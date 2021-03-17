using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.DTO.Book.Books;
using bookshelf.DTO.Create;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> Get()
        { 
            try
            {
                var results = await _repository.GetAll();
                return _mapper.Map<BookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpGet("{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> GetPageByGenre(string genre, int page)
        { 
            try
            {
                var results = await _repository.GetPageByGenre(genre, page);
                return _mapper.Map<BookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
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
        
        [Authorize]
        [HttpGet("user")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO[]>> GetBooks()
        { 
            try
            {
                var results = await _repository.GetAll();
                BookLoggedDTO[] models = _mapper.Map<BookLoggedDTO[]>(results);
                return Ok(models);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [Authorize]
        [HttpGet("user/{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO[]>> GetPageByGenreForLogged(string genre, int page)
        { 
            try
            {
                var results = await _repository.GetPageByGenre(genre, page);
                return _mapper.Map<BookLoggedDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [Authorize]
        [HttpGet("user/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookLoggedDTO>> GetBook(Guid id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null) return NotFound();
                return _mapper.Map<BookLoggedDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        //[Authorize]
        [HttpGet("user/UserBooks")]
        [Produces("application/json")]
        //change to dto
        public async Task<ActionResult<UserBookDTO[]>> GetUserBooks()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                var result = await _repository.GetUserBooks(user.Id);
                if (result == null) return NotFound();
                return _mapper.Map<UserBookDTO[]>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        [Authorize]
        [HttpGet("user/UserBooks/{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserBookDTO[]>> Get(string genre, int page)
        { 
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                var results = await _repository.GetAllUserBooksByGenre(user.Id, genre, page);
                return _mapper.Map<UserBookDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [Authorize]
        [HttpGet("user/UserBooks/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserBookDTO>> GetUserBook(Guid id)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                var result = await _repository.GetUserBookById(user.Id, id);
                if (result == null) return NotFound();
                return _mapper.Map<UserBookDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        /*[Authorize]*/
        [HttpGet("user/search={search}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> GetUserBookBySearch(string search, int page)
        {
            try
            {
                var result = await _repository.GetBySearch(search, page);
                if (result == null) return NotFound();
                return _mapper.Map<BookDTO[]>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        //change to dto
        /*[Authorize]*/
        [HttpGet("user/UserBooks/{id}/city={city}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserBook[]>> GetUserBookByCity(Guid id, int page, string city)
        {
            try
            {
                var result = await _repository.GetAllUserBooksByCity(id, page, city);
                if (result == null) return NotFound();
                return result;
                //return _mapper.Map<UserBook[]>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [Authorize]
        [HttpPost("user/UserBooks/{id}/add")]
        public async Task<ActionResult<Review>> Post(Guid id, [FromBody]ReviewAddDTO reviewDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserBook userBook = await _repository.GetUserBookById(user.Id, id);
            var review = _mapper.Map<Review>(reviewDto);
            var book = await _repository.GetById(userBook.Book.Id);
            
            _repository.AddReview(new Review()
            {
                Content = review.Content,
                User = user,
                Book = book,
                Votes = 0,
                ReviewDate = DateTime.Now
            });

            return Ok(review);
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