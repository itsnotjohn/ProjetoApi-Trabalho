using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;
using Sigma.Domain.Enums;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.Data.Context;

namespace Sigma.Infra.Data.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly SigmaContext _dbContext;

        public ProjetoRepository(SigmaContext dbContext)
        {
            _dbContext = dbContext;
        }

		public async Task<List<Projeto>> ObterTodos()
            => await _dbContext.Projeto.ToListAsync();

		public async Task<Projeto> ObterPorId(long id)
            => await _dbContext.Projeto.FindAsync(id);

        public async Task<IEnumerable<Projeto>> ObterNomeStatus(string? nome, StatusProjetoEnum? status)
		{
			var query = _dbContext.Projeto.AsQueryable();

			if (!string.IsNullOrEmpty(nome))
				query = query.Where(p => p.Nome.Contains(nome));

			if (status.HasValue)
				query = query.Where(p => p.Status == status.Value);

			return await query.ToListAsync();
		}
		
        public async Task<bool> Excluir(long id)
		{
            var projeto = await _dbContext.Projeto.FindAsync(id);

            _dbContext.Projeto.Remove(projeto);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Adicionar(Projeto entidade)
        {
            if (entidade.Status == StatusProjetoEnum.Encerrado)
                entidade.DataRealTermino = DateTime.UtcNow;            

            await _dbContext.Projeto.AddAsync(entidade);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task Atualizar(Projeto projeto)
        {
            _dbContext.Projeto.Update(projeto);
            await _dbContext.SaveChangesAsync();
        }
    }
}
