using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PortfolioSite2.DTOs;

namespace PortfolioSite2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticatieController : ControllerBase {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenticatieController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager) {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostRegisterData([FromBody]  RegisterDto dto) {
        string[] roles = ["Gast", "Editor", "Admin"];

        foreach (var role in roles) {
            if (!await _roleManager.RoleExistsAsync(role)) {
                var newRole = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!newRole.Succeeded) {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { message = $"Kon rol '{role}' niet aanmaken.", errors = newRole.Errors });
                }
            }
        }

        var user = new IdentityUser{
            UserName = dto.Username,
            Email = dto.Email,
        };

        var createUser = await _userManager.CreateAsync(user, dto.Password);
        if (!createUser.Succeeded) {
            return BadRequest(createUser.Errors);
        }
        

        var giveUserRole = await _userManager.AddToRoleAsync(user, "Gast");
        if (!giveUserRole.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Gebruiker is aangemaakt, maar de rol 'Gast' kon niet worden toegekend.", errors = giveUserRole.Errors });
        }

        return Ok(new {message = $"Gebruiker ({user.UserName}, role: 'Gast') is aangemaakt."});
    }

    [HttpPost("login")]
    public async Task<IActionResult> PostLoginData([FromBody] LoginDto dto) {
        var login = await _signInManager.PasswordSignInAsync(
            userName: dto.Username,
            password: dto.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!login.Succeeded) {
            return Unauthorized(new {message = "Ongeldige login gegevens"});
        }
        
        var user = await _userManager.FindByNameAsync(dto.Username);

        if (user == null) {
            return NotFound(new {message = "Kon gegevens niet vinden."});
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        return Ok(new {message = "Succesvol ingelogd!"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteData(string id) {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null) {
            return NotFound(new { message = "Gebruiker niet gevonden, probeer een andere id." });
        }
        
        var delete = await _userManager.DeleteAsync(user);

        if (!delete.Succeeded) {
            return BadRequest(new { message = "Kon gebruiker niet verwijderen." });
        }

        return Ok(new { message = "Gebruiker is succesvol verwijderd." });

    }
}