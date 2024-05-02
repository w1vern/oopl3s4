using System.ComponentModel.DataAnnotations.Schema;

namespace oopl3s4.Models
{
    public class IsInheritance
    {
        public int Id { get; set; }
        public int ArtisanID { get; set; }
        [ForeignKey("ArtisanID")]
        public Artisan Artisan { get; set; }
        public bool isInh { get; set; }
    }
}
