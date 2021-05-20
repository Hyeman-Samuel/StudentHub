using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Domain.Identity;
using StudentHub.Infrastructure.Data;
using StudentHub.Infrastructure.Network.Email;
using StudentHub.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IEmailSender _emailSender;
        public AuthController(IAuthService authService,IEmailSender emailSender)
        {
            _authService = authService;
            _emailSender = emailSender;
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(codes: ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _authService.LoginIn(loginModel);
                if (result.HasError)
                {
                    return Response<string>(codes: ApiResponseCode.ERROR, errors: result.Errors);
                }
                return Response<AuthResult>(data: result.Data);
            }
            catch (Exception ex)
            {
                return Response<string>(codes: ApiResponseCode.ERROR, errors: ex.InnerException.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(codes: ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _authService.SignUp(signUpModel,Roles.User);
                if (result.HasError)
                {
                    return Response<string>(codes: ApiResponseCode.ERROR, errors: result.Errors);
                }
                return Response<AuthResult>(data: result.Data);
            }
            catch (Exception ex)
            {
                return Response<string>(codes: ApiResponseCode.ERROR, errors: ex.InnerException.Message);
                throw;
            }

        }

        [HttpPut]
        [Route("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(codes: ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }

            var result = await _authService.RefreshToken(refreshTokenDto);
            if (result.HasError)
            {
                return Response<string>(codes: ApiResponseCode.ERROR, errors: result.Errors);
            }

            return Response<AuthResult>(data: result.Data);
        }




        [HttpGet]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!ModelState.IsValid)
                return Response<string>(codes: ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            var result = await _authService.ValidateEmailAndGetResetPasswordToken(email);
            if (result.HasError)
            {
                return Response<string>(codes: ApiResponseCode.NOTFOUND, errors: ListModelStateErrors.ToArray());
            }
            var callback = Url.Link("https://client.com/route", new { result.Data.Token, email = result.Data.Email });
            var message = new Message(new string[] { result.Data.Email }, "Reset password token","Reset your password <p>"+callback+"</p>");
             _emailSender.SendEmail(message);
            return Response<string>(data:"done" ,message:"You received an email");

        }



        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordResetModel resetModel)
        {
            if (!ModelState.IsValid)
                return Response<string>(codes: ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            var result = await _authService.ResetPassword(resetModel);

            if (result.HasError)
            {
                return Response<string>(codes: ApiResponseCode.NOTFOUND, errors: result.Errors);
            }

            return Response<string>(data: "done", message: "Password Reset");
        }






    }
}
