using InventoryMgtApp.BLL.Services.Contracts;
using InventoryMgtApp.DAL.Entities.DTOs;
using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgtApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("UserRegistration")]
    public async Task<ActionResult> UserRegistration(RegistrationDto model)
    {
        var newUser = await _authService.UserRegisteration(model);

        if (newUser is null)
            return BadRequest();

        return Ok(newUser);
    }

    [HttpPost("AdminRegistration")]
    public async Task<ActionResult> AdminRegistration(RegistrationDto model)
    {
        var newAdmin = await _authService.AdminRegistration(model);

        if (newAdmin is null)
            return BadRequest();

        return Ok(newAdmin);
    }

    [HttpPost("GeneralLogin")]
    public async Task<ActionResult> Login(LoginDto model)
    {
        var login = await _authService.Login(model);

        if (login is null)
            return BadRequest();

        return Ok(login);
    }

    [HttpGet("GetUsers")]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _authService.GetUsers();

        if (users is null)
            NotFound();

        return Ok(users);
    }

    [HttpGet("GetUser")]
    public async Task<ActionResult> GetUser(string id)
    {
        var user = await _authService.GetUser(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("UpdateUser")]
    public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.UpdateUser(id, model);

        if (!result)
            return NotFound();


        return NoContent();
    }

    [HttpDelete("DeleteUser")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var deleteUser = await _authService.DeleteUser(id);

        if (deleteUser is null)
            return NotFound();

        return Ok(deleteUser);
    }
}