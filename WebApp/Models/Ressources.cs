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
        public virtual Type Type { get; set; }
    }
}