using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class Ressources
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime Date { get; set; }
        public string CheminAcces { get; set; }
        public string Source { get; set; }


        [ForeignKey("Type")]
        public int IdType { get; set; }
        [ForeignKey("Categorie")]
        public int IdCategorie { get; set; }
        public string NomPersonne { get; set; }
        public bool IsValidate { get; set; }
        public virtual Type Type { get; set; }
        public virtual Categorie Categorie { get; set; }

        [NotMapped]
        public int nbcom { get; set; }
    }
}