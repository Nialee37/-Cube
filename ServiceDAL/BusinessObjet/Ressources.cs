using System;
using System.ComponentModel.DataAnnotations;

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
    }
}