using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
