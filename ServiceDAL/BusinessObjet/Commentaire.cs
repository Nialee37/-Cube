using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class Commentaire
    {
        [Key]
        public int Id { get; set; }
        public string commentaire { get; set; }
        public DateTime date_com { get; set; }
        [ForeignKey("Personne")]
        public int IdPersonne { get; set; }
        [ForeignKey("Ressources")]
        public int IdRessource { get; set; }
        public bool isReponse { get; set; }
        [ForeignKey("OrigineCommentaire")]
        public int? IdCommentaireOrigine { get; set; }
        public virtual Commentaire OrigineCommentaire { get; set; }
        public virtual Personne Personne { get; set; }
        public virtual Ressources Ressources { get; set; }
    }
}
