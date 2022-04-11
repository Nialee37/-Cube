using ServiceDAL.BusinessObjet;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Ressources
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime Date { get; set; }
        public string CheminAcces { get; set; }
        public string Source { get; set; }
        public int IdType { get; set; }
        public int IdCategorie { get; set; }
        public int IdPersonne { get; set; }
        public virtual ServiceDAL.BusinessObjet.Type Type { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Personne Personne { get; set; }
    }
}