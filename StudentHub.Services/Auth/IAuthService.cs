using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Auth
{
    public interface IAuthService
    {
        Task<ResultModel<AuthResult>> SignUp(SignUpModel model);

        Task<ResultModel<AuthResult>> LoginIn(LoginModel model);

        Task<ResultModel<AuthResult>> RefreshToken(RefreshTokenDto value);

        Task<ResultModel<PasswordResetTokenHandler>> ValidateEmailAndGetResetPasswordToken(string email);
        ///void ForgotPassword();
        Task<ResultModel<AuthResult>> ResetPassword(PasswordResetModel resetmodel);

    }
}
