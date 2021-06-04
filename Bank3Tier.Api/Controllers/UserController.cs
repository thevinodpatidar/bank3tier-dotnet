using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bank3Tier.Api.Authorization;
using Bank3Tier.Core.Helpers;
using Bank3Tier.Api.Resources.Auth;
using Bank3Tier.Api.Resources.User;
using Bank3Tier.Core.Models;
using Bank3Tier.Core.Services;
using Microsoft.AspNetCore.Mvc;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Bank3Tier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public UserController(IUserService userService, IMapper mapper, IJwtUtils jwtUtils)
        {
            this._mapper = mapper;
            this._jwtUtils = jwtUtils;
            this._userService = userService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return Ok(userResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            var userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserResource>> Register([FromBody] SaveUserResource saveUserResource)
        {
            //var validator = new SaveMusicResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveMusicResource);

            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userToCreate = _mapper.Map<SaveUserResource, User>(saveUserResource);
            userToCreate.Password = BCryptNet.HashPassword(userToCreate.Password);
            var newUser = await _userService.Register(userToCreate);

            var user = await _userService.GetUserById(newUser.Id);

            var userResource = _mapper.Map<User, UserResource>(user);
            
            return Ok(userResource);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserResource>> Login([FromBody] LoginUserResource userLogin)
        {
            var user = await _userService.GetUserByUsername(userLogin.Username);

            if (user == null || !BCryptNet.Verify(userLogin.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<User, LoginUserResponseResource>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResource>> UpdateMusic(int id, [FromBody] SaveUserResource saveUserResource)
        {
            //var validator = new SaveMusicResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveMusicResource);

            //var requestIsInvalid = id == 0 || !validationResult.IsValid;

            //if (requestIsInvalid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userToBeUpdate = await _userService.GetUserById(id);

            if (userToBeUpdate == null)
                return NotFound();

            var user = _mapper.Map<SaveUserResource, User>(saveUserResource,userToBeUpdate);

            var uss = await _userService.UpdateUser(user);

            var updatedUser = await _userService.GetUserById(id);
            var updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);

            return Ok(uss);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMusic(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var music = await _musicService.GetMusicById(id);

        //    if (music == null)
        //        return NotFound();

        //    await _musicService.DeleteMusic(music);

        //    return NoContent();
        //}
    }
}
