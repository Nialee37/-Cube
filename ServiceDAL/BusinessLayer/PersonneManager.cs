using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class PersonneManager : IManager<Personne>
    {
        public Personne Add(Personne obj)
        {
            var personne = Service.DbContext.Personnes.Add(obj);
            Service.DbContext.SaveChanges();
            return personne;
        }

        public bool Delete(int id)
        {
            Personne personne = Service.DbContext.Personnes.Find(id);
            Service.DbContext.Personnes.Remove(personne);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Personne Get(int id)
        {
            return Service.DbContext.Personnes
                .Include("Adresse")
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }
        public Personne GetByName(string name)
        {
            return Service.DbContext.Personnes
                .Include("Adresse")
                .Where(p => p.Nom == name)
                .FirstOrDefault();
        }

        public Personne GetByMail(string mail)
        {
            
            return Service.DbContext.Personnes
                .Include("Adresse")
                .Where(p => p.Mail == mail)
                .FirstOrDefault();
        }

        public IList<Personne> GetAll()
        {
            return Service.DbContext.Personnes.ToList();
        }

        public void Update(Personne obj)
        {
            Personne Old = Service.DbContext.Personnes.Find(obj.Id);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose(){}
    }
}
