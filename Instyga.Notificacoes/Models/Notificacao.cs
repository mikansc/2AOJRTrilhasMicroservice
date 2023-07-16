using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notificacoes.Models
{
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