using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(2)]
    public class adresseCRUDTests
    {
        private static ServiceDAL.BusinessLayer.AdresseManager Manager { get; set; }
        [Fact]
        [Order(0)]
        public void Adresse_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.AdresseManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Adresse_CountAllAtStart_Find0()
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
        public void Adresse_CreateOneAdresse_Find2()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Ville laVille = ServiceStructuralTests.ServiceDAL.VilleManager.Get(2);
            Manager.Add(new Adresse()
            {
                Numero = "2",
                Nom = "NomAdresse",
                Type = 1,
                IdVille = 1,
                Ville = laVille
            }) ;
            Manager.Add(new Adresse()
            {
                Numero = "2Bis",
                Nom = "NomAdresse2",
                Type = 2,
                IdVille = 2,
                Ville = laVille
            }) ;
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(3)]
        public void Adresse_GetAdresse_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "NomAdresse";

            // Act
            Adresse uneAdresse = Manager.Get(ID);
            string actual = uneAdresse.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void Adresse_UpdateAdresse_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "AdresseModifié";

            // Act
            Adresse uneAdresse = Manager.Get(ID);
            uneAdresse.Nom = EXCEPTED;
            Manager.Update(uneAdresse);
            Adresse uneAdresseModif = Manager.Get(ID);
            string actual = uneAdresseModif.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Adresse_DeleteAdresse_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Adresse uneAdresse = Manager.Get(ID);
            Adresse actual = uneAdresse;

            // Assert
            Assert.Null(actual);
        }
    }
}
