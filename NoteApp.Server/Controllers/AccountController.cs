﻿using Microsoft.AspNetCore.Mvc;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;
using NoteApp.Server.Services;

namespace NoteApp.Server.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerUserDto)
        {
            await _service.RegisterUserAsync(registerUserDto);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            string token = await _service.LoginAsync(loginUserDto);

            return Ok(token);
        }

    }
}
