using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Ville
    {
        [Key]
        public int IdVille { get; set; }
        [Display(Name = "Ville :", AutoGenerateFilter = false)]
        public string Nom { get; set; }
        public string CPostal { get; set; }
    }
}