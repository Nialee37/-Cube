using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class VilleManager : IManager<Ville>
    {
        internal VilleManager() { }

        public Ville Add(Ville obj)
        {
            var ville = Service.DbContext.Villes.Add(obj);
            Service.DbContext.SaveChanges();
            return ville;
        }

        public bool Delete(int id)
        {
            Ville ville = Service.DbContext.Villes.Find(id);
            Service.DbContext.Villes.Remove(ville);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Ville Get(int id)
        {
            return Service.DbContext.Villes.Find(id);
        }

        public IList<Ville> GetAll()
        {
            return Service.DbContext.Villes.ToList();
        }

        public IList<Ville> Get30last()
        {
            return Service.DbContext.Villes.OrderBy(x => x.CPostal).Take(30).ToList();
        }

        public IList<Ville> GetbyCPOrVille(string cpName)
        {
            return Service.DbContext.Villes.Where(x =>x.CPostal.Contains(cpName) || x.Nom.Contains(cpName)).Take(30).ToList();
        }

        public void Update(Ville obj)
        {
            Ville Old = Service.DbContext.Villes.Find(obj.IdVille);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
        }
    }
}
