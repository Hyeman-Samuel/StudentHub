﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Domain.Identity;
using StudentHub.Infrastructure;
using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        
        private readonly ApplicationDBContext context;
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthService(UserManager<ApplicationUser> _userManager,
            ApplicationDBContext _context,
            TokenValidationParameters _tokenValidationParameters,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            context = _context;
            tokenValidationParameters = _tokenValidationParameters;
            roleManager = _roleManager;
        }
        public async Task<ResultModel<AuthResult>> LoginIn(LoginModel loginModel)
        {
            //initialize result model
            var result = new ResultModel<AuthResult>();

            if (string.IsNullOrEmpty(loginModel.Username))
            {
                result.AddError("User not found");
                return result;
            }
            var UserFoundByUsername = await userManager.FindByNameAsync(loginModel.Username);
            var UserFoundByEmail = await userManager.FindByEmailAsync(loginModel.Username);
            if(UserFoundByEmail != null)
            {
                result.Data = await GenerateJWTToken(UserFoundByEmail);
            }else if (UserFoundByUsername != null)
            {
                result.Data = await GenerateJWTToken(UserFoundByUsername);
            }
            else
            {
                result.AddError("Invalid Username/Password");
                return result;
            }

            return result;
        }

        public async Task<ResultModel<AuthResult>> RefreshToken(RefreshTokenDto value)
        {
            var result = new ResultModel<AuthResult>();

            var validToken = GetPrincipalFromToken(value.Token);
            if (validToken == null)
            {
                result.AddError("Invalid Token");
                return result;
            }

            var expirydate = long.Parse(validToken.Claims.Single(p => p.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirytimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expirydate);
            if (expirytimeUtc > DateTime.UtcNow)
            {
                result.AddError("Token is still valid");
                return result;
            }

            var jti = validToken.Claims.Single(p => p.Type == JwtRegisteredClaimNames.Jti).Value;

            var stored = await context.RefreshToken.SingleOrDefaultAsync(p => p.Token == value.RefreshToken);
            if (stored == null)
            {
                result.AddError("Refresh Token does not exist");
                return result;
            }

            if (DateTime.UtcNow > stored.ExpiryDate)
            {
                result.AddError("Refresh Token has not expired");
                return result;
            }

            if (stored.IsUsed)
            {
                result.AddError("Refresh Token have been used");
                return result;
            }

            if (stored.JwtId != jti)
            {
                result.AddError("Refresh Token does not match");
                return result;
            }

            stored.IsUsed = true;
            context.Update(stored);
            await context.SaveChangesAsync();

            var email = validToken.Claims.Single(p => p.Type == "sub").Value;
            var appuser = await userManager.FindByEmailAsync(email);

            result.Data = await GenerateJWTToken(appuser);
            return result;
        }

        public async Task<ResultModel<AuthResult>> SignUp(SignUpModel model,string role)
        {
            var result = new ResultModel<AuthResult>();
            if (String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.Password))
            {
                result.AddError("Invalid Login Attempt");
            }
            var user = new ApplicationUser{
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            UserName = model.Username};
            try
            {
                if (roleManager.FindByNameAsync(role) == null)
                {
                   await roleManager.CreateAsync(new IdentityRole(role));                  
                }; 

            var _user = await userManager.CreateAsync(user, model.Password);
                
                
               
                if (_user.Succeeded)
                {    
                    var addedToRow = await userManager.AddToRoleAsync(user, role);
                    if (!addedToRow.Succeeded)
                {
                    foreach (var error in addedToRow.Errors)
                    {
                       var deleteUser = await userManager.DeleteAsync(user);
                            if (!deleteUser.Succeeded)
                            {
                                deleteUser = await userManager.DeleteAsync(user);
                            }
                        result.AddError("Server Error:" + error.Description);
                    }
                    }
                    else
                    {
                      result.Data = await GenerateJWTToken(user);
                    }

                } else {
                    foreach (var error in _user.Errors)
                    {
                        result.AddError("Server Error:"+error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError("Server Error:"+ ex.Message);                
            }

            return result;
        }

        public async Task<ResultModel<PasswordResetTokenHandler>> ValidateEmailAndGetResetPasswordToken(string email)
        {
            var result = new ResultModel<PasswordResetTokenHandler>();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {           
                result.AddError("User Not Found");
                return result;
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            result.Data = new PasswordResetTokenHandler {Token = token, Email = user.Email};
            return result;

        }

        public async Task<ResultModel<AuthResult>> ResetPassword(PasswordResetModel resetModel)
        {
            var result = new ResultModel<AuthResult>();
            try
            {
                var user = await userManager.FindByEmailAsync(resetModel.Email);
                if (user == null) {
                    result.AddError("User Not Found");
                    return result;
                }
                   
                var resetPassResult = await userManager.ResetPasswordAsync(user, resetModel.Token, resetModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        result.AddError(error.Code + " :" + error.Description);
                    }
                    return result;
                }

               var authResult = await GenerateJWTToken(user);
                    result.Data = authResult;
                    return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsValidAlgorithm(validatedToken))
                    return null;
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool IsValidAlgorithm(SecurityToken securityToken)
        {
            return (securityToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthResult> GenerateJWTToken(ApplicationUser user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MySuperSecureKey"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                  new Claim("UserId",user.Id),
                  new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                }),
                Issuer = "issue",
                Audience = "audience",
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(signinKey,SecurityAlgorithms.HmacSha256)
                
            };

            var roles = await userManager.GetRolesAsync(user);
            if (roles != null) {
                foreach (var role in roles)
                {
                    var claim = new Claim(ClaimTypes.Role, role);
                    tokenDescriptor.Subject.Claims.Append(claim);
                }
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(5),
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id
            };

             context.RefreshToken.Add(refreshToken);
            await context.SaveChangesAsync();

            return new AuthResult { ExpiryTimeinMinutes = (token.ValidTo - token.ValidFrom).TotalMinutes.ToString(), Token = tokenHandler.WriteToken(token), RefreshToken = refreshToken.Token };
        }
    }
}
