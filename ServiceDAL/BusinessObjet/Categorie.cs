using System.ComponentModel.DataAnnotations;

namespace ServiceDAL.BusinessObjet
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
