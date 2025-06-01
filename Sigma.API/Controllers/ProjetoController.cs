using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Enums;

namespace Sigma.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projetos")]
    public class ProjetoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProjetoService _projetoService;

        public ProjetoController(IConfiguration configuration, IProjetoService projetoService)
        {
            _configuration = configuration;
            _projetoService = projetoService;
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarProjetos([FromQuery] string? nome, [FromQuery] StatusProjetoEnum? status)
        {
            if (string.IsNullOrEmpty(nome) && status == null)
            {
                var todosProjetos = await _projetoService.ObterTodos();
                return Ok(todosProjetos);
            }

            var projetosFiltrados = await _projetoService.ConsultarNomeStatus(nome, status);
            return Ok(projetosFiltrados);
        }

        [Authorize]
        [HttpPost("inserir")]
        public async Task<IActionResult> CriarProjeto([FromBody] ProjetoNovoDto model)
        {
            var resultado = await _projetoService.Adicionar(model);
            return Ok(resultado);
        }

        [Authorize]
        [HttpPut("atualizar/{id:long}")]
        public async Task<IActionResult> AtualizarProjeto(long id, [FromBody] ProjetoNovoDto dto)
        {
            await _projetoService.Alterar(id, dto);
            return Ok();
        }

        [Authorize]
        [HttpDelete("remover/{id:long}")]
        public async Task<IActionResult> RemoverProjeto(long id)
        {
            try
            {
                await _projetoService.Excluir(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
