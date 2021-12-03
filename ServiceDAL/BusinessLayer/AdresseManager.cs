using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDAL.BusinessLayer
{
    public class AdresseManager : IManager<Adresse>
    {
        public Adresse Add(Adresse obj)
        {
            try
            {

                var adresse = Service.DbContext.Adresses.Add(obj);
                Service.DbContext.SaveChanges();
                return adresse;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"erreur add : {ex.Message}");
                throw;
            }
        }

        public bool Delete(int id)
        {
            Adresse adresse = Service.DbContext.Adresses.Find(id);
            Service.DbContext.Adresses.Remove(adresse);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Adresse Get(int id)
        {
            return Service.DbContext.Adresses.Find(id);
        }

        public IList<Adresse> GetAll()
        {
            return Service.DbContext.Adresses.ToList();
        }

        public void Update(Adresse obj)
        {
            Adresse Old = Service.DbContext.Adresses.Find(obj.IdAdresse);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose(){}
    }
}
