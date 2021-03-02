using System;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DAL;
using bookshelf.DTO.Create;
using bookshelf.DTO.Read;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace bookshelf_app.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/users")]
    
    public class UsersController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<User> _userManager;

        public UsersController(IBaseRepository<User> _repository, IMapper _mapper, LinkGenerator linkGenerator, UserManager<User> userManager)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
        }
        
        // [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        public async Task<ActionResult<UserReadDTO[]>> GetAll()
        {
            try
            {
                User[] results = await _repository.GetAll();
                return _mapper.Map<UserReadDTO[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUser(Guid  id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null) return NotFound();
                return _mapper.Map<UserReadDTO>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Post(UserCreateDTO model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                var location = _linkGenerator.GetPathByAction("GetUser", "Users", new { id = user.Id });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }
                user.RegistrationDate = DateTime.Now;
                // user.NormalizedEmail = model.Email.Normalize();

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user");
                }
                
                _repository.Add(user);

                // await _repository.Commit();
                return Created(location, _mapper.Map<UserReadDTO>(user));
                
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