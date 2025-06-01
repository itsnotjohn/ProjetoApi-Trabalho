using Sigma.Application.Dtos;
using Sigma.Domain.Dtos;
using Sigma.Domain.Enums;

namespace Sigma.Application.Interfaces
{
    public interface IProjetoService
    {
        Task<bool> Adicionar(ProjetoNovoDto model);

        Task<List<ProjetoDto>> ObterTodos();

        Task<IEnumerable<ProjetoDto>> ConsultarNomeStatus(string? nome, StatusProjetoEnum? status);

        Task<bool> Excluir(long id);

		Task<bool> Alterar(long id, ProjetoNovoDto dto);
	}
}