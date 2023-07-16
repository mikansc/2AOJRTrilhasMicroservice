using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Avaliacoes.Models
{
    public class Avaliacao : Model
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeTrilha { get; set; }

    }

    public class Notificacao : Model
    {
        public int Id { get; set; }
        public string Trilha { get; set; }
        public string Mensagem { get; set; }

    }

    public abstract class Model
    {
        public int Id { get; set; }
    }
}