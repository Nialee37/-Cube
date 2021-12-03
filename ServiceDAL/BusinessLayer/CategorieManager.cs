using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class CategorieManager : IManager<Categorie>
    {
        public Categorie Add(Categorie obj)
        {
            var categorie = Service.DbContext.Categorie.Add(obj);
            Service.DbContext.SaveChanges();
            return categorie;
        }

        public bool Delete(int id)
        {
            Categorie categorie = Service.DbContext.Categorie.Find(id);
            Service.DbContext.Categorie.Remove(categorie);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Categorie Get(int id)
        {
            return Service.DbContext.Categorie
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public IList<Categorie> GetAll()
        {
            return Service.DbContext.Categorie.ToList();
        }

        public void Update(Categorie obj)
        {
            Categorie Old = Service.DbContext.Categorie.Find(obj.Id);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose() { }
    }
}
