using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oopl3s4.Models
{
    public class Emails
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public int ArtisanID { get; set; }
        [ForeignKey("ArtisanID")]
        public Artisan Artisan { get; set; }
    }
}
