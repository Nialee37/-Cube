using System.ComponentModel.DataAnnotations;

namespace ServiceDAL.BusinessObjet
{
    public class Adresse
    {
        [Key]
        public int IdAdresse { get; set; }
        public string Numero { get; set; }
        public string Nom { get; set; }
        public int Type { get; set; }
        public int IdVille { get; set; }
        public virtual Ville Ville { get; set; }
    }
}