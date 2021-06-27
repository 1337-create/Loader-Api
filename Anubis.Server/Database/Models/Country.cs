using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anubis.Server.Database.Models
{
    [Table("countries")]
    public class Country
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("short_code")]
        public string ShortCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("region_id")]
        public int RegionId { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; }
    }
}
