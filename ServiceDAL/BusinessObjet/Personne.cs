using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDAL.BusinessObjet
{
    public class Personne
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }        
        public int Genre { get; set; }
        public string PasswordHash { get; set; }
        public string IconProfile { get; set; }
        public string Mail { get; set; }
        [ForeignKey("Adresse")]
        public int IdAdresse { get; set; }
        [ForeignKey("Roles")]
        public int IdRoles { get; set; }
        public bool IsActivate { get; set; }
        public virtual Adresse Adresse { get; set; }
        public virtual Roles Roles { get; set; }
    }
}