using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TrilhaId { get; set; }
    }

    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}