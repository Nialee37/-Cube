using System.ComponentModel.DataAnnotations;

namespace ServiceDAL.BusinessObjet
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
