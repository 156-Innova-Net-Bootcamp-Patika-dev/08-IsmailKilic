using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration _configuration,
            SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this._configuration = _configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user,
                    request.Password, true, true);

                if (user != null && result.Succeeded == true)
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Sid, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return new LoginCommandResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Roles = userRoles,
                        User = mapper.Map<UserVM>(user),
                    };
                }
                throw new Exception("Wrong username or password");
            }

            return new LoginCommandResponse
            {
                Token = "",
                Message = "Login failed"
            };


        }
    }
}
