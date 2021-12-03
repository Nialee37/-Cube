using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class RolesManager : IManager<Roles>
    {
        public Roles Add(Roles obj)
        {
            var roles = Service.DbContext.Roles.Add(obj);
            Service.DbContext.SaveChanges();
            return roles;
        }

        public bool Delete(int id)
        {
            Roles roles = Service.DbContext.Roles.Find(id);
            Service.DbContext.Roles.Remove(roles);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Roles Get(int id)
        {
            return Service.DbContext.Roles
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public IList<Roles> GetAll()
        {
            return Service.DbContext.Roles.ToList();
        }

        public void Update(Roles obj)
        {
            Roles Old = Service.DbContext.Roles.Find(obj.Id);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose() { }
    }
}
