using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trilhas.Models
{
    public class Trilha : Model
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public abstract class Model
    {
        public int Id { get; set; }
    }
}