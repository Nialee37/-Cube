using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class LinkRessCatManager : IManager<LinkRessCat>
    {
        public LinkRessCat Add(LinkRessCat obj)
        {
            var linkRessCat = Service.DbContext.LinkRessCat.Add(obj);
            Service.DbContext.SaveChanges();
            return linkRessCat;
        }
        public bool Delete(int id)
        {
            LinkRessCat linkRessCat = Service.DbContext.LinkRessCat.Find(id);
            Service.DbContext.LinkRessCat.Remove(linkRessCat);
            Service.DbContext.SaveChanges();
            return true;
        }
        public LinkRessCat Get(int id)
        {
            throw new System.NotImplementedException();
        }
        public IList<LinkRessCat> GetAll()
        {
            return Service.DbContext.LinkRessCat.ToList();
        }
        public void Update(LinkRessCat obj)
        {
            LinkRessCat Old = Service.DbContext.LinkRessCat.Find(obj.IdRessource);
            LinkRessCat Old2 = Service.DbContext.LinkRessCat.Find(obj.IdCategorie);
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
