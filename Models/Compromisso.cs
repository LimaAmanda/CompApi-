using System;
using System.Collections.Generic;
using CompAPI.Models;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CompApi.Models
{

    [Table("TB_COMPROMISSO")]
    public class Compromisso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escolhha o tipo de compromisso")]

        public int TipoCompromissoId { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo descrição deve contem até 30 caracteres")]
        [MinLength(10, ErrorMessage = "O campo descrição deve contem pelo menos até 10 caracteres")]
        public string Descricao {get; set; }
        public string Localizacao { get; set;}
        
        [Required(ErrorMessage = "A data de inicio é obrigatória")]
        public DateTime DataInicio { get; set;}
        public DateTime DataTermino {get; set; }
        public bool Visivel {get; set; }
        public int participanteId{get; set;}

        public List<Participante> participantes {get; set;}
    }
}