using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.Data.Context;

namespace Sigma.Infra.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SigmaContext _dbContext;

        public LoginRepository(SigmaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Login> ObterUsuario(string usuario)
        {
            if(string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("O nome de usuário não pode ser vazio ou nulo.");

            return await _dbContext.Login
            .Where(l => l.Usuario == usuario)
            .Select(l => new Login { Usuario = l.Usuario, Senha = l.Senha })
            .FirstOrDefaultAsync();
        }

        public async Task<bool> Adicionar(Login login)
        {
            await _dbContext.Login.AddAsync(login);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
