using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Error
    {
        public Error(int code_erreur, string message)
        {
            this.code_erreur = code_erreur;
            this.message = message;
        }
        [Key]
        public int code_erreur { get; set; }   
        public string message { get; set; }
    }
}
