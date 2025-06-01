using Sigma.Domain.Entities;

namespace Sigma.Domain.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> Adicionar(Login login);

        Task<Login> ObterUsuario(string usuario);
    }
}
