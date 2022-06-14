using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(3)]
    public class RoleCRUDTests
    {
        private static ServiceDAL.BusinessLayer.RolesManager Manager { get; set; }


        [Fact]
        [Order(0)]
        public void Role_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.RolesManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Role_CountAllAtStart_Find0()
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
        public void Role_CreateTwoRole_Find2()
        {
            // Arrange
            const long EXCEPTED = 5;

            // Act
            Manager.Add(new Roles(){Libelle = "Role1"});
            Manager.Add(new Roles() { Libelle = "Citoyen" });
            Manager.Add(new Roles() { Libelle = "Modérateur" });
            Manager.Add(new Roles() { Libelle = "Administrateur" });
            Manager.Add(new Roles() { Libelle = "SuperAdministrateur" });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(3)]
        public void Roles_GetRoles_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "Role1";

            // Act
            Roles unRoles = Manager.Get(ID);
            string actual = unRoles.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void Roles_UpdateRoles_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "RolesModifié";

            // Act
            Roles unRoles = Manager.Get(ID);
            unRoles.Libelle = EXCEPTED;
            Manager.Update(unRoles);
            Roles unRolesModif = Manager.Get(ID);
            string actual = unRolesModif.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Roles_DeleteRoles_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Roles unRolesModif = Manager.Get(ID);
            Roles actual = unRolesModif;

            // Assert
            Assert.Null(actual);
        }
    }
}
