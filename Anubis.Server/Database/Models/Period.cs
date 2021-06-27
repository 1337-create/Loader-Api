using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anubis.Server.Database.Models
{
    [Table("key_periods")]
    public class Period
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("period_value")]
        public string PeriodValue { get; set; }
    }
}
