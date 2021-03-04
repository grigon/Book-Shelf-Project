using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.DTO.Book.Books;
using bookshelf.Model.Books;
using Microsoft.AspNetCore.Http;
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

        public BookController(BookRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
             _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        
        //for not logged/registered users
        /*[HttpGet("genre")]
        [Produces("application/json")]*/
       //Change to book DTO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /*public async Task<ActionResult<Book[][]>> Get(int i/*string genre#1#)
        { 
            try
            {
                var results = await _repository.GetAll(i/*genre#1#);
                //return _mapper.Map<BookDTO[]>(results);
                return results;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }*/
        
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
        [HttpGet("logged/UserBooks/{id}/{genre}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserBookDTO[]>> GetUserBooks(string genre, Guid id)
        {
            try
            {
                var result = await _repository.GetAllUserBooks(id, genre);
                if (result == null) return NotFound();
                return _mapper.Map<UserBookDTO[]>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        
        [HttpGet("{genre}/page={page}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> Get(int page, string genre)
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