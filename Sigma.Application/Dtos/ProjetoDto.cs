using Sigma.Domain.Enums;

namespace Sigma.Application.Dtos
{
    public class ProjetoDto
    {
        public long? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
		public decimal? Orcamento { get; set; }
		public DateTime? DataInicio { get; set; }
        public DateTime? PrevisaoTermino { get; set; }
        public DateTime? DataRealTermino { get; set; }
		public ClassificacaoRiscoEnum? Risco { get; set; }
		public StatusProjetoEnum? Status { get; set; }
	}
}
