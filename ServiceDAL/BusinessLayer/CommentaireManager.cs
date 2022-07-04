using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDAL.BusinessLayer
{
    public class CommentaireManager : IManager<Commentaire>
    {
        public Commentaire Add(Commentaire obj)
        {
            var commentaire = Service.DbContext.Commentaire.Add(obj);
            Service.DbContext.SaveChanges();
            return commentaire;
        }

        public bool Delete(int id)
        {
            Commentaire commentaire = Service.DbContext.Commentaire.Find(id);
            Service.DbContext.Commentaire.Remove(commentaire);
            Service.DbContext.SaveChanges();
            return true;
        }

        public Commentaire Get(int id)
        {
            return Service.DbContext.Commentaire
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public IList<Commentaire> GetCommentaireByRessource(int id)
        {
            return Service.DbContext.Commentaire
                .Where(p => p.IdRessource == id)
                .ToList();
        }

        public IList<Commentaire> GetAll()
        {
            return Service.DbContext.Commentaire.ToList();
        }

        public void Update(Commentaire obj)
        {
            Commentaire Old = Service.DbContext.Commentaire.Find(obj.Id);
            if (Old != null)
            {
                Service.DbContext.Entry(Old).CurrentValues.SetValues(obj);
                Service.DbContext.SaveChanges();
            }
        }

        public void Dispose() { }
    }
}
