using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
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

        public string Mail { get; set; }
        public int Genre { get; set; }
        public string PasswordHash { get; set; }

        public int IdAdresse { get; set; }
        public int IdRoles { get; set; }
        public bool IsActivate { get; set; }
        public virtual Adresse Adresse { get; set; }
        public virtual Roles Roles { get; set; }
    }
}