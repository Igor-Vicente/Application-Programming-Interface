using Business.Layer.Interfaces;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;
using Client.Layer.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client.Layer.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthController(INotificator notificator,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<AppSettings> appSettings, IUser user) : base(notificator, user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> RegisterUser(CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddClaimAsync(user, new Claim("Supplier", "Read"));
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(await GenerateToken(createUserDto.Email));
            }
            else
            {
                foreach (var erro in result.Errors) NotifierError(erro.Description);
                return CustomResponse();
            }
        }

        [HttpPost("signin-account")]
        public async Task<IActionResult> SignInUser(SigninUserDto signinUserDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _signInManager.PasswordSignInAsync(signinUserDto.Email, signinUserDto.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateToken(signinUserDto.Email));
            }
            if (result.IsLockedOut)
            {
                NotifierError("The user has been temporarily blocked due to invalid attempts");
                return CustomResponse();
            }
            NotifierError("Username or password is invalid");
            return CustomResponse();
        }

        private async Task<OutSigninDto> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var role in userRoles) claims.Add(new Claim("role", role.ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Host,
                Audience = _appSettings.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);
            var response = new OutSigninDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                TimePeriod = "Seconds",
                UserToken = new UserTokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                }
            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
