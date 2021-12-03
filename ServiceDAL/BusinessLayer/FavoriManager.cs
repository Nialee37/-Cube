using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class FavoriManager : IManager<Favori>
    {

        public Favori Add(Favori obj)
        {
            var favori = Service.DbContext.Favori.Add(obj);
            Service.DbContext.SaveChanges();
            return favori;
        }
        public bool Delete(int id)
        {
            Favori favori = Service.DbContext.Favori.Find(id);
            Service.DbContext.Favori.Remove(favori);
            Service.DbContext.SaveChanges();
            return true;
        }


        public Favori Get(int idP)
        {
            return Service.DbContext.Favori
                .Where(p => p.IdPersonne == idP)
                .FirstOrDefault();
        }

        public Favori Get(int idP, int idR)
        {
            return Service.DbContext.Favori
                .Where(p => p.IdPersonne == idP && p.IdRessource == idR)
                .FirstOrDefault();
        }
        public IList<Favori> GetAll()
        {
            return Service.DbContext.Favori.ToList();
        }

        public void Update(Favori obj)
        {
            Favori Old = Service.DbContext.Favori.Find(obj.IdRessource);
            Favori Old2 = Service.DbContext.Favori.Find(obj.IdPersonne);
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
