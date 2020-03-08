using System.Security.Cryptography;
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
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<UserForReturnDto>> Register([FromBody] UserForRegisterDto userForRegisterDto)
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
        public async Task<ActionResult<LoginForReturnDto>> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.UserName == userForLoginDto.Username);

            if (user == null)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid username or password.", 401, Request));
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid username or password.", 401, Request));
            }

            var token = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens.RemoveAll(token => token.Source == userForLoginDto.Source || token.Expiration < DateTime.Now);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Source = userForLoginDto.Source,
                Expiration = DateTime.Now.AddMinutes(authSettings.RefreshTokenExpirationTimeInMinutes)
            });

            await userManager.UpdateAsync(user);

            var userToReturn = mapper.Map<UserForReturnDto>(user);

            return Ok(new LoginForReturnDto
            {
                Token = token,
                RefreshToken = refreshToken,
                User = userToReturn
            });
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            // Still validate the passed in token, but ignore its expiration date by setting validate lifetime to false
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.APISecrect)),
                RequireSignedTokens = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = false,
                ValidAudience = authSettings.TokenAudience,
                ValidIssuer = authSettings.TokenIssuer
            };

            ClaimsPrincipal tokenClaims;

            try
            {
                tokenClaims = new JwtSecurityTokenHandler().ValidateToken(refreshTokenDto.Token, validationParameters, out var rawValidatedToken);
            }
            catch (Exception e)
            {
                return Unauthorized(new ProblemDetailsWithErrors(e.Message, 401, Request));
            }

            var userIdClaim = tokenClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid token.", 401, Request));
            }

            var user = await userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid token.", 401, Request));
            }

            if (user.RefreshTokens.FirstOrDefault(rToken => rToken.Token == refreshTokenDto.RefreshToken && rToken.Source == refreshTokenDto.Source && rToken.Expiration >= DateTime.Now) == null)
            {
                return Unauthorized(new ProblemDetailsWithErrors("Invalid token.", 401, Request));
            }

            var token = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens.RemoveAll(rToken => rToken.Token == refreshTokenDto.RefreshToken || rToken.Source == refreshTokenDto.Source || rToken.Expiration < DateTime.Now);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Source = refreshTokenDto.Source,
                Expiration = DateTime.Now.AddMinutes(authSettings.RefreshTokenExpirationTimeInMinutes)
            });

            await userManager.UpdateAsync(user);

            return Ok(new
            {
                token,
                refreshToken
            });
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
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