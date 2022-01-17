using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class Adresse
    {
        [Key]
        public int IdAdresse { get; set; }
        public string Numero { get; set; }
        public string Nom { get; set; }
        public int Type { get; set; }

        [ForeignKey("Ville")]
        public int IdVille { get; set; }
        public virtual Ville Ville { get; set; }
    }
}