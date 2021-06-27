using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Server.Database.Models
{
    [Table("keys")]
    public class Key
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("hash")]
        public string Hash { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }
        
        [Column("packet_id")]
        public int PacketId { get; set; }
        
        [Column("hardware")]
        public string Hardware { get; set; }
        
        [Column("period")]
        public long PeriodId { get; set; }
        
        [Column("region")]
        public int RegionId { get; set; }
        
        [Column("is_activated")]
        public bool IsActivated { get; set; }
        
        [Column("is_locked")]
        public bool IsLocked { get; set; }
        
        [Column("activate_date")]
        public DateTime? ActivateDate { get; set; }
        
        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
    }
}
