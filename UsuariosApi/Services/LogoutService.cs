using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signManager;

        public LogoutService(SignInManager<IdentityUser<int>> signManager)
        {
            _signManager = signManager;
        }

        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();

            return Result.Fail("Logout falhou!");
        }
    }
}
