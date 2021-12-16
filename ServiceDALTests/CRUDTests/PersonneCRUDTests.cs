using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using System;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(4)]
    public class PersonneCRUDTests
    {
        private static ServiceDAL.BusinessLayer.PersonneManager Manager { get; set; }
        [Fact]
        [Order(0)]
        public void Personne_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.PersonneManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Personne_CountAllAtStart_Find0()
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
        public void Personne_CreateOnePersonne_Find1()
        {
            // Arrange
            const long EXCEPTED = 1;

            // Act

            Adresse UneAdresse = ServiceStructuralTests.ServiceDAL.AdresseManager.Get(2);
            Roles UnRoles = ServiceStructuralTests.ServiceDAL.RolesManager.Get(2);
            Manager.Add(new Personne()
            {
                Nom = "leNom",
                Prenom = "lePrenom",
                DateNaissance = new DateTime(2010, 8, 18),
                Genre = 1,
                IdAdresse = 1,
                IdRoles = 1,
                Adresse = UneAdresse,
                Roles = UnRoles
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(4)]
        public void Personne_GetPersonne_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "leNom";

            // Act
            Personne laPersonne = Manager.Get(ID);
            string actual = laPersonne.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Personne_UpdatePersonne_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "villeModifié";

            // Act
            Personne laPersonne = Manager.Get(ID);
            laPersonne.Nom = EXCEPTED;
            Manager.Update(laPersonne);
            Personne laPersonneModif = Manager.Get(ID);
            string actual = laPersonneModif.Nom;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        //[Fact]
        //[Order(5)]
        //public void Personne_UpdatePersonneWithAdresse_NameOk()
        //{
        //    Arrange
        //    const int ID = 1;
        //    const string EXCEPTED = "nomAdresseModifié";

        //    Act
        //   Personne laPersonne = Manager.GetWithDependencies(ID);
        //    laPersonne.Adresse.Nom = EXCEPTED;
        //    Manager.UpdateWithAdresse(laPersonne);
        //    Personne laPersonneModif = Manager.GetWithDependencies(ID);
        //    string actual = laPersonneModif.Adresse.Nom;

        //    Assert
        //    Assert.Equal(EXCEPTED, actual);
        //}
        [Fact]
        [Order(6)]
        public void Personne_DeletePersonne_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Personne unePersonne = Manager.Get(ID);
            Personne actual = unePersonne;

            // Assert
            Assert.Null(actual);
        }
    }
}
