using ServiceDAL.BusinessObjet;
using ServiceDAL.Migrations;
using System.Data.Entity;

namespace ServiceDAL.AccessLayer
{
    class ServiceContext: DbContext
    {
        private static ServiceContext instance;

        public static ServiceContext Instance()
        {
            if (instance == null)
            {
                instance = new ServiceContext();
                instance.Database.Initialize(true);
            }
            return instance;
        }

        public ServiceContext()
             //: base("Server=172.16.3.1; Database=project; User Id=project ; Password=Fvo$i%3y$3Y4b6VZ; Trusted_Connection=false; MultipleActiveResultSets=true;")
             : base("server=(localdb)\\mssqllocaldb;database=ServiceDal_Local;Trusted_Connection=True;MultipleActiveResultSets=true;")

        {
            SetConfiguration();
        }

        public ServiceContext(string connectionString)
             : base(connectionString)
        {
            SetConfiguration();
        }

        private void SetConfiguration()
        {
            //Création de la bdd si non existante
            Database.SetInitializer<ServiceContext>(new CreateDatabaseIfNotExists<ServiceContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ServiceContext, Configuration>());
            Configuration.LazyLoadingEnabled = true;
        }
        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Personne> Personnes { get; set; }
        public virtual DbSet<Commentaire> Commentaire { get; set; }
        public virtual DbSet<Ressources> Ressources { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<Historique> Historique { get; set; }
        public virtual DbSet<Favori> Favori { get; set; }
        public virtual DbSet<Archive> Archive { get; set; }
        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }

        #region Required
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ressources>()
                    .HasRequired(c => c.Personne)
                    .WithMany()
                    .WillCascadeOnDelete(false);
        }
        #endregion

    }
}
