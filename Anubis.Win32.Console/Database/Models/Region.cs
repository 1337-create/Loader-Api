using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anubis.Win32.Server.Database.Models
{
    [Table( "regions" )]
    public class Region
    {
        [Key]
        [Column( "id" )]
        public long Id { get; set; }

        [Column( "name" )]
        public string Name { get; set; }

        [Column( "is_locked" )]
        public bool IsLocked { get; set; }
    }
}
