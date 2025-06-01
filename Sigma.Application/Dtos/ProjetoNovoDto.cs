using Sigma.Domain.Enums;

namespace Sigma.Domain.Dtos
{
    public class ProjetoNovoDto
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Orcamento { get; set; }
		public DateTime? DataInicio { get; set; }
		public DateTime? PrevisaoTermino { get; set; }
		public ClassificacaoRiscoEnum? Risco { get; set; }
        public StatusProjetoEnum? Status { get; set; }
    }
}
