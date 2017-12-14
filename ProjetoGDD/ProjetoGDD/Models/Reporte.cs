using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoGDD.Models
{
    public class Reporte
    {
        private readonly TimesDB TimesContext = new TimesDB();

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string[] Membros { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [BsonIgnoreIfNull]
        [Display(Name = "Data de Criação")]
        public String DataCriacao { get; set;}

        [BsonIgnoreIfNull]
        public List<Resposta> Respostas { get; set; }

        public Tuple<string,List<string>> MembrosRestantes
        {
            get
            {
                var time = TimesContext.Times.FindAll().SetSortOrder(SortBy<Time>.Descending(r => r.Id)).FirstOrDefault();
                var membros = time.Membros;

                if(this.Respostas != null)
                {
                    var membrosRespostas = this.Respostas.Select(m => m.Nome).ToList();

                    foreach (var nome in membrosRespostas)
                    {
                        var target = membros.Single(m => m == nome);
                        membros.Remove(target);
                    }
                }
               
                return new Tuple<string,List<string>>(String.Join(", ", membros),membros);
            }  
        }
        
    }

    
}