using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Server.Entities;
using NoteApp.Server.Exceptions;
using NoteApp.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteApp.Server.Services
{
    // Account service interface
    public interface IAccountService
    {
        public Task RegisterUserAsync(RegisterUserDto registerUserDto);
        public Task<string> LoginAsync(LoginUserDto loginUserDto);
    }
    public class AccountService : IAccountService
    {
        private readonly NoteAppContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(NoteAppContext dbContext, IMapper mapper, 
            IPasswordHasher<User> passwordhasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordhasher;
            _authenticationSettings = authenticationSettings;
        }

        // HTTP Post - register new user
        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = _mapper.Map<User>(registerUserDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        // HTTP Post - login user and return token
        public async Task<string> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null)
                throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName.ToString()} {user.LastName.ToString()}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var newToken = tokenHandler.WriteToken(token);

            return newToken;
        }
    }
}
