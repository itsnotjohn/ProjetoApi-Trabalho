using System.ComponentModel.DataAnnotations;

namespace Sigma.Domain.Enums
{
    public enum ClassificacaoRiscoEnum
	{
        [Display(Name = "Baixo")]
        Baixo,
        [Display(Name = "Médio")]
        Medio,
        [Display(Name = "Alto")]
        Alto
    }
}