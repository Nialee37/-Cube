using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceDAL.BusinessObjet
{
    public class Commentaire
    {
        [Key]
        public int Id { get; set; }
        public long commentaire { get; set; }
        public int IdPersonne { get; set; }
        public int IdRessource { get; set; }
        public virtual Personne Personne { get; set; }
        public virtual Ressources Ressources { get; set; }
    }
}
