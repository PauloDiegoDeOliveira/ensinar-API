using System.ComponentModel.DataAnnotations;

namespace MinhaPrimeiraAPI.Models.Base
{
    // "Data annotations" atributo para validar.
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public string Status { get; set; }

        public DateTime? CriadoEm { get; set; } = DateTime.Now;

        public DateTime? AlteradoEm { get; set; }
    }
}