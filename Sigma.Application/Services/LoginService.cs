using AutoMapper;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repositories;

namespace Sigma.Application.Services
{
	public class LoginService : ILoginService
	{
		private readonly IMapper _mapper;
		private readonly ILoginRepository _loginRepository;

		public LoginService(IMapper mapper, ILoginRepository loginRepository)
		{
			_mapper = mapper;
			_loginRepository = loginRepository;
		}

		public async Task<bool> Adicionar(LoginDto model)
		{
			var entidade = _mapper.Map<Login>(model);
			var resultado = await _loginRepository.Adicionar(entidade);
			return resultado;
		}

		public async Task<Login> ObterLoginPorUsuario(string usuario)
		{
			var login = await _loginRepository.ObterUsuario(usuario);
			return login;
		}
	}
}
