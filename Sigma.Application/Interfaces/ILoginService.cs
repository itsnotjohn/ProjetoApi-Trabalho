using Sigma.Application.Dtos;
using Sigma.Domain.Entities;

namespace Sigma.Application.Interfaces
{
    public interface ILoginService
    {
        Task<bool> Adicionar(LoginDto model);

        Task<Login> ObterLoginPorUsuario(string usuario);
    }
}