using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.User;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IBaseRepository<User> _repository, IMapper _mapper, LinkGenerator linkGenerator)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            _linkGenerator = linkGenerator;
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

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            try
            {
                
                var user = _mapper.Map<User>(model);
                var location = _linkGenerator.GetPathByAction("GetUser", "Users", new { id = user.Id });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }
                _repository.Add(user);
                if (await _repository.Commit())
                {
                    return Created(location, _mapper.Map<UserModel>(user));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var oldUser = await _repository.GetById(Id);
                if (oldUser == null) return NotFound();
                _repository.Remove(oldUser);

                if (await _repository.Commit())
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            
            return BadRequest("Failed to delete user");
        }
    }
}