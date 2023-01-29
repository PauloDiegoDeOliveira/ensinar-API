using MinhaPrimeiraAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaPrimeiraAPI.Models
{
    // "Model" contém ou ou representa os dados com os quais a aplicação vai tratar,
    // sendo responsável pela leitura e escrita de dados e também de suas validações.

    [Table("Participantes")]
    public class Participante : EntityBase
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPF { get; set; }
    }
}