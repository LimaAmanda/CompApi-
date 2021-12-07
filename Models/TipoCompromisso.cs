using Dapper.Contrib.Extensions;

namespace CompApi.Models
{
    [Table("TB_TIPO_COMPROMISSO")]
    public class TipoCompromisso
    {
        [Key]
        public int Id { get; set; }

        public string DescricaoCompromisso { get; set; }
    }
}