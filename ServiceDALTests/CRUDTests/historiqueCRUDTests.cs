using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using System;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(10)]
    public class historiqueCRUDTests
    {
        private static ServiceDAL.BusinessLayer.HistoriqueManager Manager { get; set; }
        [Fact]
        [Order(0)]
        public void Historique_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.HistoriqueManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Historique_CountAllAtStart_Find0()
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
        public void Historique_CreateOneArchive_Find1()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Personne laPersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(2);
            Personne laSecondePersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(3);
            Ressources laRessources = ServiceStructuralTests.ServiceDAL.RessourcesManager.Get(2);
            Manager.Add(new Historique()
            {
                Personne = laPersonne,
                Ressources = laRessources,
                Date = DateTime.Now
        });
            Manager.Add(new Historique()
            {
                Personne = laSecondePersonne,
                Ressources = laRessources,
                Date = DateTime.Now
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(3)]
        public void Historique_GetArchive_NameOk()
        {
            // Arrange
            const int ID = 2;
            const int EXCEPTED = 2;

            // Act
            Historique unHistorique = Manager.Get(ID);
            int actual = unHistorique.IdPersonne;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void Historique_DeleteAdresse_null()
        {
            // Arrange
            const int IDP = 2;
            const int IDR = 2;

            // Act
            Manager.DeleteByPersAndRess(IDP,IDR);
            Historique unHistorique = Manager.Get(IDP,IDR);
            Historique actual = unHistorique;

            // Assert
            Assert.Null(actual);
        }

    }
}
