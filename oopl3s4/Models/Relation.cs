using System.ComponentModel.DataAnnotations.Schema;

namespace oopl3s4.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int ArtisanID { get; set; }
        [ForeignKey("ArtisanID")]
        public Artisan Artisan { get; set; }
        public int CraftID { get; set; }
        [ForeignKey("CraftID")]
        public Craft Craft { get; set; }
    }
}
