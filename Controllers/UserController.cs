using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sampleapi.Model.BindingModel;
using sampleapi.Data.Entities;
using sampleapi.Model.DTO;
using sampleapi.Model;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using sampleapi.Enum;

namespace sampleapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
    

        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTConfig _jwtconfig;

        public UserController(ILogger<UserController> logger,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IOptions<JWTConfig> JwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtconfig = JwtConfig.Value;
        }
        [HttpPost("RegisterUser")]
        public async Task<object>RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model )
        {
            try
            {
                var user = new AppUser { FullName = model.FullName, Email = model.Email,UserName=model.Email, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow };
                var resut = await _userManager.CreateAsync(user, model.Password);
                if (resut.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User has been registerd",null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"success", string.Join(",", resut.Errors.Select(a => a.Description).ToArray())));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message,null));
            }
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                var users = _userManager.Users.Select(a=>new UserDTO(a.FullName,a.Email,a.UserName,a.DateCreated));
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "success", users));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if(model.Email=="" || model.Password == "") { return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Parameters are missing",null)); }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var appuser = await _userManager.FindByEmailAsync(model.Email);
                    var user = new UserDTO(appuser.FullName, appuser.Email, appuser.UserName, appuser.DateCreated);
                    user.Token = generateToken(appuser);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Login SuccessFull", user));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR,"Login failed",null));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }
        private string generateToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtconfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer=_jwtconfig.Issuer,
                Audience=_jwtconfig.Audience
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
