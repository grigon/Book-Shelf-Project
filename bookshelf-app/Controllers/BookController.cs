using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.Entity;
using bookshelf.Model.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace bookshelf_app.Controllers
{
    public class BookController
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
        
        /*public async Task<ActionResult<BookModel[]>> Get(bool includeTalks = false)
        {
            //if (false) return this.NotFound("Bad stuff happens");
            /*try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);
                CampModel[] models = _mapper.Map<CampModel[]>(results);
                return Ok(models);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Databae failure");
            }#1#
        }*/
    }
}