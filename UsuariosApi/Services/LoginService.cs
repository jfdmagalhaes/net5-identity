using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Requests;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signManager;

        public LoginService(SignInManager<IdentityUser<int>> signManager)
        {
            _signManager = signManager;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("login falhou!!");
        }
    }
}