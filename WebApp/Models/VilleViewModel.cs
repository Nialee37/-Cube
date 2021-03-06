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
        public string code_departement { get; set; }
        public string code_commune { get; set; }
    }
}