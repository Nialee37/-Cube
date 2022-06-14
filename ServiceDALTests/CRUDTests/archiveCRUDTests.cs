using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(9)]
    public class archiveCRUDTests
    {
        private static ServiceDAL.BusinessLayer.ArchiveManager Manager { get; set; }
         [Fact]
         [Order(0)]
        public void Archive_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.ArchiveManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Archive_CountAllAtStart_Find0()
        {
            // Arrange
            const long EXCEPTED = 0;

            // Act
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(2)]
        public void Archive_CreateOneArchive_Find1()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Personne laPersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(2);
            Personne laSecondePersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(3);
            Ressources laRessources = ServiceStructuralTests.ServiceDAL.RessourcesManager.Get(2);
            Manager.Add(new Archive()
            {
                Personne = laPersonne,
                Ressources = laRessources
            });
            Manager.Add(new Archive()
            {
                Personne = laSecondePersonne,
                Ressources = laRessources
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(3)]
        public void Archive_GetArchive_NameOk()
        {
            // Arrange
            const int ID = 2;
            const int EXCEPTED = 2;

            // Act
            Archive uneArchive = Manager.Get(ID);
            int actual = uneArchive.IdPersonne;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Adresse_DeleteAdresse_null()
        {
            // Arrange
            const int ID = 2;

            // Act
            Manager.Delete(ID);
            Archive uneArchive = Manager.Get(ID);
            Archive actual = uneArchive;

            // Assert
            Assert.Null(actual);
        }

    }
}
