﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.DTO.Book.Books;
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
        [HttpGet("genre={genre}")]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO[]>> Get(string genre)
        { 
            try
            {
                var results = await _repository.GetAll(genre);
                BookDTO[] models = _mapper.Map<BookDTO[]>(results);
                return Ok(models);
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
                var results = await _repository.GetAll(genre);
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
                var result = await _repository.GetById(id);
                if (result == null) return NotFound();
                return _mapper.Map<BookLoggedDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}