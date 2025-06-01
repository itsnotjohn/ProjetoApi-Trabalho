using AutoMapper;
using Sigma.Application.Dtos;
using Sigma.Domain.Entities;

namespace Sigma.Application.Mappers
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<LoginDto, Login>();
        }
    }
}