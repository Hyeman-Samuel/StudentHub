using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
                var result = await _authService.SignUp(signUpModel);
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
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string username)
        {
            return Ok();
        }

        [HttpPut]
        [Route("PasswordResetRedirect")]
        public IActionResult PasswordResetRedirect([FromBody]string Email)
        {
            return Ok();
        }



    }
}
