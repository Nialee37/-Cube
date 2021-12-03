
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class Historique
    {
        [Key][Column(Order = 0)]
        public int IdPersonne { get; set; }
        [Key][Column(Order = 1)]
        public int IdRessource { get; set; }
        public DateTime Date { get; set; }
        public virtual Personne Personne { get; set; }
        public virtual Ressources Ressources { get; set; }
    }
}