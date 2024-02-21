using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;

namespace NoteApp.Server.Services
{
    public interface IAccountService
    {
        public Task RegisterUserAsync(RegisterUserDto registerUserDto);
    }
    public class AccountService : IAccountService
    {
        private readonly NoteAppContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(NoteAppContext dbContext, IMapper mapper, IPasswordHasher<User> passwordhasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordhasher;
        }
        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = _mapper.Map<User>(registerUserDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
