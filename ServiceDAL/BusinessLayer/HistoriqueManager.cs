using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class HistoriqueManager : IManager<Historique>
    {
        public Historique Add(Historique obj)
        {
            var historique = Service.DbContext.Historique.Add(obj);
            Service.DbContext.SaveChanges();
            return historique;
        }
        public bool DeleteByPersAndRess(int idpersonne , int idressource)
        {
            Historique historique = Service.DbContext.Historique.Where(p => p.IdPersonne == idpersonne && p.IdRessource == idressource).FirstOrDefault();
            Service.DbContext.Historique.Remove(historique);
            Service.DbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Historique historique = Service.DbContext.Historique.Find(id);
            Service.DbContext.Historique.Remove(historique);
            Service.DbContext.SaveChanges();
            return true;
        }

        public List<Historique> Getall(int idP)
        {
             List<Historique> historiques = Service.DbContext.Historique
                .Where(p => p.IdPersonne == idP)
                .ToList();
            return historiques;
        }
        public Historique Get(int idP)
        {
            return Service.DbContext.Historique.Where(p => p.IdPersonne == idP).First();     
        }
        public Historique Get(int idP, int idR)
        {
            return Service.DbContext.Historique
                .Where(p => p.IdPersonne == idP && p.IdRessource == idR)
                .FirstOrDefault();
        }
        public IList<Historique> GetAll()
        {
            return Service.DbContext.Historique.ToList();
        }

        public void Update(Historique obj)
        {
            Historique Old = Service.DbContext.Historique.Find(obj.IdPersonne);
            Historique Old2 = Service.DbContext.Historique.Find(obj.IdRessource);
            if (Old != null && Old2 != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.Entry(Old2).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }
        public void Dispose() { }
    }
}
