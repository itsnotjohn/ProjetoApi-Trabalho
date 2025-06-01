using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;

namespace Sigma.API.Controllers
{
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;

        public AutenticacaoController(ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> Adicionar([FromBody] LoginDto model)
        {
            var usuario = await _loginService.ObterLoginPorUsuario(model.Usuario);
            if (usuario != null)
                return Unauthorized("O usuário informado já consta cadastrado.");

            var resultado = await _loginService.Adicionar(model);
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var usuario = await _loginService.ObterLoginPorUsuario(login.Usuario);
            if (usuario == null)
                return Unauthorized("Credenciais inválidas ou não encontradas.");

            var token = _tokenService.GerarToken(usuario.Usuario);
            return Ok(new { token });
        }
    }
}
