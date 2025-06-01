using Sigma.Domain.Entities;
using Sigma.Domain.Enums;

namespace Sigma.Domain.Interfaces.Repositories
{
    public interface IProjetoRepository
    {
		Task<Projeto> ObterPorId(long id);

        Task<bool> Adicionar(Projeto entidade);

        Task Atualizar(Projeto projeto);

        Task<List<Projeto>> ObterTodos();

        Task<IEnumerable<Projeto>> ObterNomeStatus(string? nome, StatusProjetoEnum? status);

        Task<bool> Excluir(long id);
	}
}
