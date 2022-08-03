using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Pessoas 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite um nome válido."), MaxLength(200)]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite um CPF válido."), MaxLength(11)]
        public string Cpf { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite uma idade válida.") ]
        public int Idade { get; set; }

        [ForeignKey(nameof(Id_Cidade))]
        [InverseProperty(nameof(Cidades.Pessoas))]
        public virtual Cidades Cidade { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite uma cidade válida.")]
        public int Id_Cidade { get; set; }

        public Pessoas(Pessoas @object)
        {

        }
        public Pessoas()
        {

        }
    }
}
