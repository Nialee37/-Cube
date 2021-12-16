using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using System;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(6)]
    public class CategorieCRUDTests
    {
        private static ServiceDAL.BusinessLayer.CategorieManager Manager { get; set; }
        [Fact]
        [Order(0)]
        public void Categorie_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.CategorieManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Categorie_CountAllAtStart_Find0()
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
        public void Categorie_CreateTwoCategorie_Find2()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act

            Manager.Add(new Categorie()
            {
                Libelle = "Categorie1"
            });
            Manager.Add(new Categorie()
            {
                Libelle = "Categorie2"
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(4)]
        public void Categorie_GetPCategorie_NameOk()
        {
            // Arrange
            const int ID = 2;
            const string EXCEPTED = "Categorie2";

            // Act
            Categorie laCategorie = Manager.Get(ID);
            string actual = laCategorie.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Categorie_UpdateCategorie_NameOk()
        {
            // Arrange
            const int ID = 2;
            const string EXCEPTED = "CategorieModifié";

            // Act
            Categorie laCategorie = Manager.Get(ID);
            laCategorie.Libelle = EXCEPTED;
            Manager.Update(laCategorie);
            Categorie laCategorieModif = Manager.Get(ID);
            string actual = laCategorieModif.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(6)]
        public void Categorie_DeleteCategorie_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Categorie uneCategorie = Manager.Get(ID);
            Categorie actual = uneCategorie;

            // Assert
            Assert.Null(actual);
        }
    }
}
