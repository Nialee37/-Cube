using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(1)]
    public class VilleCRUDTests
    {
        private static ServiceDAL.BusinessLayer.VilleManager Manager { get; set; }


        [Fact]
        [Order(0)]
        public void Ville_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.VilleManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Ville_CountAllAtStart_Find0()
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
        public void Ville_CreateOneVille_Find2()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Manager.Add(new Ville() {
                Nom = "ville1",
                CPostal = "00001A"
            });
            Manager.Add(new Ville() {
                Nom = "ville2",
                CPostal = "00002B" 
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(3)]
        public void Ville_GetVille_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "ville1";

            // Act
            Ville laVille = Manager.Get(ID);
            string actual = laVille.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void Ville_UpdateVille_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "villeModifié";

            // Act
            Ville uneVille = Manager.Get(ID);
            uneVille.Nom = EXCEPTED;
            Manager.Update(uneVille);
            Ville uneVilleModif = Manager.Get(ID);            
            string actual = uneVilleModif.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Ville_DeleteVille_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Ville uneVilleModif = Manager.Get(ID);
            Ville actual = uneVilleModif;

            // Assert
            Assert.Null(actual);
        }
    }
}
