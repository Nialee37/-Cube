using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using System;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(7)]
    public class RessourceCRUDTests
    {
        private static ServiceDAL.BusinessLayer.RessourcesManager Manager { get; set; }
        [Fact]
        [Order(0)]
        public void Ressource_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.RessourcesManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Ressources_CountAllAtStart_Find0()
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
        public void Ressources_Create10Ressources_Find10()
        {
            // Arrange
            const long EXCEPTED = 10;

            // Act

            ServiceDAL.BusinessObjet.Type UnType = ServiceStructuralTests.ServiceDAL.TypeManager.Get(2);
            Categorie UneCategorie = ServiceStructuralTests.ServiceDAL.CategorieManager.Get(2);
            Personne UnePersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(2);
            for (int i = 0; i < 5; i++)
            {
                Manager.Add(new Ressources()
                {
                    Nom = "UneRessource"+i,
                    Date = new DateTime(2010, 8, 18),
                    CheminAcces = "unCheminDacces"+i,
                    Source = "uneSource"+i,
                    IsValidate = true,
                    IdType = 2,
                    Type = UnType,
                    IdCategorie = 2,
                    Categorie = UneCategorie,
                    IdPersonne = 2,
                    Personne = UnePersonne

                });
                Manager.Add(new Ressources()
                {
                    Nom = "UneRessource" + i,
                    Date = new DateTime(2010, 8, 18),
                    CheminAcces = "unCheminDacces" + i,
                    Source = "uneSource" + i,
                    IsValidate = false,
                    IdType = 2,
                    Type = UnType,
                    IdCategorie = 2,
                    Categorie = UneCategorie,
                    IdPersonne = 2,
                    Personne = UnePersonne

                });

            }
          
          
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(3)]
        public void Ressource_GetPRessource_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "UneRessource0";

            // Act
            Ressources laRessource = Manager.Get(ID);
            string actual = laRessource.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(4)]
        public void Ressource_GetAllFalse_NameOk()
        {
            // Arrange
            const int EXCEPTED = 5;

            // Act
            long actual = Manager.Getallfalse().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Ressource_Get10last_NameOk()
        {
            // Arrange
            const int EXCEPTED = 5;

            // Act
            long actual = Manager.Getallfalse().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(6)]
        public void Ressource_UpdateRessource_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "RessourceModifié";

            // Act
            Ressources laRessource = Manager.Get(ID);
            laRessource.Nom = EXCEPTED;
            Manager.Update(laRessource);
            Ressources laRessourceModif = Manager.Get(ID);
            string actual = laRessourceModif.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(7)]
        public void Ressource_DeleteRessource_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Ressources uneRessource = Manager.Get(ID);
            Ressources actual = uneRessource;

            // Assert
            Assert.Null(actual);
        }
    }
}
