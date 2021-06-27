using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anubis.Win32.Server.Database.Models
{
    [Table( "key_groups" )]
    public class Group
    {
        [Key]
        [Column( "id" )]
        public long Id { get; set; }

        [Column( "owner_id" )]
        public int OwnerId { get; set; }

        [Column( "name" )]
        public string Name { get; set; }

        [Column( "key_count" )]
        public int KeysCount { get; set; }

        [Column( "is_locked" )]
        public bool IsLocked { get; set; }
    }
}
