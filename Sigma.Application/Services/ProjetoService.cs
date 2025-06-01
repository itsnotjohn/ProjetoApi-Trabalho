using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Entities;
using Sigma.Domain.Enums;
using Sigma.Domain.Interfaces.Repositories;

namespace Sigma.Application.Services
{
	public class ProjetoService : IProjetoService
	{
		private readonly IMapper _mapper;
		private readonly IProjetoRepository _projetoRepository;

		private readonly StatusProjetoEnum[] statusSemExclusao = new[]
		{
			StatusProjetoEnum.Iniciado,
			StatusProjetoEnum.Planejado,
			StatusProjetoEnum.EmAndamento,
			StatusProjetoEnum.Encerrado
		};

		public ProjetoService(IMapper mapper, IProjetoRepository projetoRepository)
		{
			_mapper = mapper;
			_projetoRepository = projetoRepository;
		}

        public async Task<bool> Alterar(long id, ProjetoNovoDto dto)
        {
            var projeto = await _projetoRepository.ObterPorId(id);
            if (projeto == null)
                return false;

            projeto.Nome = dto.Nome;
            projeto.Descricao = dto.Descricao;
            projeto.DataInicio = dto.DataInicio;
            projeto.PrevisaoTermino = dto.PrevisaoTermino;
            projeto.Orcamento = dto.Orcamento ?? 0;
            projeto.Risco = dto.Risco ?? 0;
            projeto.Status = dto.Status ?? 0;
            projeto.DataRealTermino = projeto.Status == StatusProjetoEnum.Encerrado ? DateTime.UtcNow : null;

            await _projetoRepository.Atualizar(projeto);
            return true;
        }

        public async Task<IEnumerable<ProjetoDto>> ConsultarNomeStatus(string? nome, StatusProjetoEnum? status)
		{
			var projetos = await _projetoRepository.ObterNomeStatus(nome, status);
			return _mapper.Map<IEnumerable<ProjetoDto>>(projetos);
		}

		public async Task<bool> Excluir(long id)
		{
			if (id <= 0 || id == null)
				throw new ArgumentException("ID do projeto é inexistente.");

			var projeto = await _projetoRepository.ObterPorId(id)  ?? throw new KeyNotFoundException("Não foi possível encontrar o projeto");

			if (statusSemExclusao.Contains(projeto.Status))
				throw new InvalidOperationException($"Não é possível realizar a exclusão de projetos com status: '{projeto.Status}'.");

			await _projetoRepository.Excluir(id);
			return true;
		}

		public async Task<bool> Adicionar(ProjetoNovoDto model)
		{
			var projeto = _mapper.Map<Projeto>(model);
			return await _projetoRepository.Adicionar(projeto);
		}

		public async Task<List<ProjetoDto>> ObterTodos()
		{
			var projetos = await _projetoRepository.ObterTodos();
			return _mapper.Map<List<ProjetoDto>>(projetos);
		}
	}
}
