using ServiceDAL.AccessLayer;
using ServiceDAL.BusinessLayer;
using ServiceDAL.Interfaces;

namespace ServiceDAL
{
    public class Service : IService
    {
        internal static ServiceContext DbContext { get; set; }

        public Service(string connectionString = "")
        {
            if (connectionString == "")
                DbContext = new ServiceContext();
            else
                DbContext = new ServiceContext(connectionString);

            RolesManager = new RolesManager();
            RessourcesManager = new RessourcesManager();
            VilleManager = new VilleManager();
            CategorieManager = new CategorieManager();
            TypeManager = new TypeManager();
            AdresseManager = new AdresseManager();
            PersonneManager = new PersonneManager();
        }

        public RolesManager RolesManager { get; private set; }
        public RessourcesManager RessourcesManager { get; private set; }
        public VilleManager VilleManager { get; private set; }
        public CategorieManager CategorieManager { get; private set; }
        public TypeManager TypeManager { get; private set; }
        public AdresseManager AdresseManager { get; private set; }
        public PersonneManager PersonneManager { get; private set; }

        public void Dispose()
        {
            RolesManager.Dispose();
            RessourcesManager.Dispose();
            VilleManager.Dispose();
            CategorieManager.Dispose();
            TypeManager.Dispose();
            AdresseManager.Dispose();
            PersonneManager.Dispose();
            DbContext.Dispose();
        }
    }
}
