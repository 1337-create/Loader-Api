using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anubis.Server.Database.Models
{
    [Table("access")]
    public class Access
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("xor_key")]
        public string XorKey { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; }

        [Column("disabled_message")]
        public string DisabledMessage { get; set; }
    }
}
