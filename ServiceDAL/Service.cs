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
            VilleManager = new VilleManager();
            AdresseManager = new AdresseManager();
            PersonneManager = new PersonneManager();
        }

        public RolesManager RolesManager { get; private set; }
        public VilleManager VilleManager { get; private set; }
        public AdresseManager AdresseManager { get; private set; }
        public PersonneManager PersonneManager { get; private set; }

        public void Dispose()
        {
            RolesManager.Dispose();
            VilleManager.Dispose();
            AdresseManager.Dispose();
            PersonneManager.Dispose();
            DbContext.Dispose();
        }
    }
}
