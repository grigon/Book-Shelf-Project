using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Book;
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
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public BookController(IBaseRepository<Book> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
             _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        
        [HttpGet]
        public async Task<ActionResult<BookDTO[]>> Get()
        { 
            try
            {
                var results = await _repository.GetAll();
                BookDTO[] models = _mapper.Map<BookDTO[]>(results);
                return Ok(models);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}