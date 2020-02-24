using System.Threading.Tasks;
using WorkoutApp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WorkoutApp.API.Models.Settings;
using Microsoft.Extensions.Options;
using WorkoutApp.API.Helpers;
using System.Linq;
using WorkoutApp.API.Models.Dtos;

namespace WorkoutApp.API.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly AuthenticationSettings authSettings;
        private readonly IMapper mapper;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AuthenticationSettings> authSettings, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authSettings = authSettings.Value;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            User userToCreate = mapper.Map<User>(userForRegisterDto);

            var result = await userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            UserForReturnDto userToReturn = mapper.Map<UserForReturnDto>(userToCreate);

            if (!result.Succeeded)
            {
                return BadRequest(new ProblemDetailsWithErrors(result.Errors.Select(e => e.Description).ToList(), 400, Request));
            }

            return CreatedAtRoute("GetUser", new { controller = "Users", id = userToCreate.Id }, userToReturn);
        }

        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns>200 with user object on success. 401 on failure.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            User userFromRepo = await userManager.FindByNameAsync(userForLoginDto.Username);

            if (userFromRepo == null)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid username or password.", 401, Request));
            }

            var result = await signInManager.CheckPasswordSignInAsync(userFromRepo, userForLoginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid username or password.", 401, Request));
            }

            UserForReturnDto user = mapper.Map<UserForReturnDto>(userFromRepo);

            string token = await GenerateJwtToken(userFromRepo);

            return Ok(new
            {
                token,
                user
            });
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            IList<string> roles = await userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.APISecrect));

            if (key.KeySize < 128)
            {
                throw new Exception("API Secret must be longer");
            }

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(authSettings.TokenExpirationTimeInMinutes),
                NotBefore = DateTime.Now,
                SigningCredentials = creds,
                Audience = authSettings.TokenAudience,
                Issuer = authSettings.TokenIssuer
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}