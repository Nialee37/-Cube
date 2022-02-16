using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class RessourcesManager : IManager<Ressources>
    {
        public Ressources Add(Ressources obj)
        {
            var ressources = Service.DbContext.Ressources.Add(obj);
            Service.DbContext.SaveChanges();
            return ressources;
        }

        public bool Delete(int id)
        {
            Ressources ressources = Service.DbContext.Ressources.Find(id);
            Service.DbContext.Ressources.Remove(ressources);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Ressources Get(int id)
        {
            return Service.DbContext.Ressources
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }



        public IList<Ressources> GetAll()
        {
            return Service.DbContext.Ressources.ToList();
        }
        public IList<Ressources> Getallfalse()
        {
            return Service.DbContext.Ressources.Where(x => x.IsValidate == false).ToList();
        }
        //public IList<Ressources> GetAllByIdPersonne(int id )
        //{
        //    return Service.DbContext.Ressources.ToList().Where(x => x.CreatorId == id);
        //}
        public IList<Ressources> Get10last()
        {
            return Service.DbContext.Ressources.OrderByDescending(x => x.Date).Take(10).ToList();
        }

        public void Update(Ressources obj)
        {
            Ressources Old = Service.DbContext.Ressources.Find(obj.Id);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose() { }
    }
}
