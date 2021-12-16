using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(5)]
    public class TypeCRUDTests
    {
        private static ServiceDAL.BusinessLayer.TypeManager Manager { get; set; }


        [Fact]
        [Order(0)]
        public void Type_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.TypeManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Type_CountAllAtStart_Find0()
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
        public void Type_CreateTwoType_Find2()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Manager.Add(new Type()
            {
                Libelle = "Type1"
            });
            Manager.Add(new Type()
            {
                Libelle = "Type2"
            });
            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }

        [Fact]
        [Order(3)]
        public void Type_GetType_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "Type1";

            // Act
            Type unType = Manager.Get(ID);
            string actual = unType.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void Type_UpdateType_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "TypesModifié";

            // Act
            Type unType = Manager.Get(ID);
            unType.Libelle = EXCEPTED;
            Manager.Update(unType);
            Type unTypeModif = Manager.Get(ID);
            string actual = unTypeModif.Libelle;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void Type_DeleteType_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Type unTypeModif = Manager.Get(ID);
            Type actual = unTypeModif;

            // Assert
            Assert.Null(actual);
        }
    }
}
