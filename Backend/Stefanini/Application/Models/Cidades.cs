using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Cidades
    {

        public Cidades()
        {
            Pessoas = new HashSet<Pessoas>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite um nome válido."), MaxLength(200)]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite um CPF válido."), MaxLength(2)]
        public string UF { get; set; }

        [InverseProperty("Cidade")]
        public virtual ICollection<Pessoas> Pessoas { get; set; }
    }
}
