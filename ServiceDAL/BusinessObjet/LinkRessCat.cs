using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class LinkRessCat
    {
        [Key][Column(Order = 0)]
        public int IdCategorie { get; set; }
        [Key][Column(Order = 1)]
        public int IdRessource { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Ressources Ressources { get; set; }
    }
}
