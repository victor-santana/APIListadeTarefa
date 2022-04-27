using System;
using System.ComponentModel.DataAnnotations;

namespace listaToDoAPI.Data.Dtos
{
    public class ReadTarefaDto
    {
        [Key]
        [Required]
        public int Id { get; internal set; }
        [Required(ErrorMessage = "O Campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo Concluidi é obrigatório")]
        public bool Concluida { get; set; }

        [Required(ErrorMessage = "O Campo Data é obrigatório")]
        public string Data { get; set; }

        public DateTime HoraDaConsulta { get; set; }
    }
}
