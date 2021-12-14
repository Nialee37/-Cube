using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class VilleViewModel
    {
        [Key]
        public int IdVille { get; set; }
        [Display(Name = "Ville :", AutoGenerateFilter = false)]
        public string Nom { get; set; }
        public string CPostal { get; set; }
    }
}