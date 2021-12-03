using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class ArchiveManager : IManager<Archive>
    {
        public Archive Add(Archive obj)
        {
            var archive = Service.DbContext.Archive.Add(obj);
            Service.DbContext.SaveChanges();
            return archive;
        }
        public bool Delete(int id)
        {
            Archive archive = Service.DbContext.Archive.Find(id);
            Service.DbContext.Archive.Remove(archive);
            Service.DbContext.SaveChanges();
            return true;
        }


        public Archive Get(int idP)
        {
            return Service.DbContext.Archive
                .Where(p => p.IdPersonne == idP)
                .FirstOrDefault();
        }

        public Archive Get(int idP, int idR)
        {
            return Service.DbContext.Archive
                .Where(p => p.IdPersonne == idP && p.IdRessource == idR)
                .FirstOrDefault();
        }
        public IList<Archive> GetAll()
        {
            return Service.DbContext.Archive.ToList();
        }

        public void Update(Archive obj)
        {
            Archive Old = Service.DbContext.Archive.Find(obj.IdPersonne);
            Archive Old2 = Service.DbContext.Archive.Find(obj.IdRessource);
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
