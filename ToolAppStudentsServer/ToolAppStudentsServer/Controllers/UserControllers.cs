using Microsoft.AspNetCore.Mvc;
using ToolAppStudentsServer.Service;
using ToolAppStudentsServer.Models;
using ToolAppStudentsServer.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ToolAppStudentsServer.Service;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToolAppStudentsServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserControllers:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserControllers(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var registeredUser=await _userService.RegisterUserAsync(registerDto);
            return CreatedAtAction(nameof(Register),new {id=registeredUser.Id},registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginUserAsync(loginDto);
            if (user == null) { return Unauthorized(); }

            string tokenString = CreateToken(new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) });
            string refreshToken = GenerateRefreshToken();

            var loginResponse = new LoginResponse
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"])),
                Username = user.Username
            };
            return Ok(loginResponse);
        }
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(LoginResponse loginResponse)
        {
            if (loginResponse is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = loginResponse.AccessToken;
            string? refreshToken = loginResponse.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var user = await _userService.FindByNameAsync(loginResponse.Username);

            if (user == null  || loginResponse.Expiration <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToArray());
            var newRefreshToken = GenerateRefreshToken();

            LoginResponse newLoginResponse = new LoginResponse
            {
                Username = user.Username,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"]))
            };
            return Ok(newLoginResponse);
        }
        [HttpGet("get_weektask")]
        [Authorize]
        public IActionResult GetWeekTasks()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); 
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid user ID.");
            }
            var tasks = _userService.GetWeekTask(userId);
            if (tasks == null) { return Unauthorized(); };
            return Ok(tasks);
        }
        [HttpPost("set_task")]
        public async Task<IActionResult> SetTask([FromQuery] int userId, [FromBody] TaskDto taskDto)
        {
            var task = await _userService.SetTaskAsync(userId, taskDto);
            return CreatedAtAction(nameof(SetTask), new { id = task.Id }, task);
        }

        private string CreateToken(Claim[] claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:TokenValidityInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng=RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
