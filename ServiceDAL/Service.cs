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

           
            
            AdresseManager = new AdresseManager();
            ArchiveManager = new ArchiveManager();
            CategorieManager = new CategorieManager();
            CommentaireManager = new CommentaireManager();
            PersonneManager = new PersonneManager();
            RessourcesManager = new RessourcesManager();
            RolesManager = new RolesManager();
            TypeManager = new TypeManager();
            VilleManager = new VilleManager();
            HistoriqueManager = new HistoriqueManager();
            FavoriManager = new FavoriManager();

        }

        public AdresseManager AdresseManager { get; private set; }
        public ArchiveManager ArchiveManager { get; private set; }
        public CategorieManager CategorieManager { get; private set; }
        public PersonneManager PersonneManager { get; private set; }
        public RessourcesManager RessourcesManager { get; private set; }
        public RolesManager RolesManager { get; private set; }
        public TypeManager TypeManager { get; private set; }
        public VilleManager VilleManager { get; private set; }
        public HistoriqueManager HistoriqueManager { get; private set; }
        public FavoriManager FavoriManager { get; private set; }
        public CommentaireManager CommentaireManager { get; private set; }



        public void Dispose()
        {
            RolesManager.Dispose();
            FavoriManager.Dispose();
            HistoriqueManager.Dispose();
            RessourcesManager.Dispose();
            CommentaireManager.Dispose();
            VilleManager.Dispose();
            CategorieManager.Dispose();
            TypeManager.Dispose();
            AdresseManager.Dispose();
            PersonneManager.Dispose();
            DbContext.Dispose();
        }
    }
}
