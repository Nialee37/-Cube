using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Adresse
    {
        [Key]
        public int IdAdresse { get; set; }
        public string Numero { get; set; }
        public string Nom { get; set; }
        public int Type { get; set; }
        public int IdVille { get; set; }
        public virtual VilleViewModel Ville { get; set; }
    }
}