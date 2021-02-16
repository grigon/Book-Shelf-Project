using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.User;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;

        public UsersController(IBaseRepository<User> _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<UserModel[]>> GetAll()
        {
            try
            {
                User[] results = await _repository.GetAll();
                return _mapper.Map<UserModel[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(Guid  id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null) return NotFound();
                return _mapper.Map<UserModel>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}