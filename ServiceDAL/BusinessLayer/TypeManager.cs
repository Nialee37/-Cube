using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class TypeManager : IManager<Type>
    {
        internal TypeManager() { }

        public Type Add(Type obj)
        {
            var type = Service.DbContext.Type.Add(obj);
            Service.DbContext.SaveChanges();
            return type;
        }

        public bool Delete(int id)
        {
            Type type = Service.DbContext.Type.Find(id);
            Service.DbContext.Type.Remove(type);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Type Get(int id)
        {
            return Service.DbContext.Type.Find(id);
        }

        public IList<Type> GetAll()
        {
            return Service.DbContext.Type.ToList();
        }

        public void Update(Type obj)
        {
            Type Old = Service.DbContext.Type.Find(obj.Id);
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
