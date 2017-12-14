using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoGDD.Models
{
    public class Resposta
    {
        [Required(ErrorMessage = "Selecione o seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o que você fez semana passada")]
        [Display(Name = "Atividade Passada:")]
        [BsonIgnoreIfNull]
        public string Atv_Passada { get; set; }

        [Required(ErrorMessage = "Informe o que você está fazendo atualmente")]
        [Display(Name = "Atividade Atual:")]
        [BsonIgnoreIfNull]
        public string Atv_Presente { get; set; }

        [Display(Name = "Atividade Futura:")]
        [BsonIgnoreIfNull]
        public string Atv_Futura { get; set; }

        [Display(Name = "Impedimentos:")]
        [BsonIgnoreIfNull]
        public string Impedimento { get; set; }
    }
}