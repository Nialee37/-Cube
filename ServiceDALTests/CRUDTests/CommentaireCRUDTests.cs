using ServiceDAL.BusinessObjet;
using ServiceDALTests.StructuralTests;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.CRUDTests
{
    [Order(8)]
    public class CommentaireCRUDTests
    {
        private static ServiceDAL.BusinessLayer.CommentaireManager Manager { get; set; }


        [Fact]
        [Order(0)]
        public void Commentaire_GetManager_Ok()
        {
            // Arrange

            // Act
            Manager = ServiceStructuralTests.ServiceDAL.CommentaireManager;

            // Assert
            Assert.NotNull(Manager);
        }

        [Fact]
        [Order(1)]
        public void Commentaire_CountAllAtStart_Find0()
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
        public void Commentaire_CreateTwoCommentaire_Find2()
        {
            // Arrange
            const long EXCEPTED = 2;

            // Act
            Personne UnePersonne = ServiceStructuralTests.ServiceDAL.PersonneManager.Get(2);
            Ressources UneRessources = ServiceStructuralTests.ServiceDAL.RessourcesManager.Get(2);
            Manager.Add(new Commentaire()
            {
                commentaire = "Commentaire1",
                date_com = System.DateTime.Now,
                IdPersonne = 1,
                IdRessource = 1,
                Personne = UnePersonne,
                Ressources = UneRessources,
                isReponse = false
            });
            Manager.Add(new Commentaire()
            {
                commentaire = "Commentaire2",
                date_com = System.DateTime.Now,
                IdPersonne = 2,
                IdRessource = 2,
                Personne = UnePersonne,
                Ressources = UneRessources,
                isReponse = false
            });

            long actual = Manager.GetAll().Count;

            // Assert
            Assert.Equal(EXCEPTED, actual);


        }

        [Fact]
        [Order(3)]
        public void commentaire_Getcommentaire_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "Commentaire1";

            // Act
            Commentaire unCommentaire = Manager.Get(ID);
            string actual = unCommentaire.commentaire;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(4)]
        public void commentaire_Updatecommentaire_NameOk()
        {
            // Arrange
            const int ID = 1;
            const string EXCEPTED = "commentaireModifié";

            // Act
            Commentaire unCommentaire = Manager.Get(ID);
            unCommentaire.commentaire = EXCEPTED;
            Manager.Update(unCommentaire);
            Commentaire unCommentaireModif = Manager.Get(ID);            
            string actual = unCommentaireModif.commentaire;

            // Assert
            Assert.Equal(EXCEPTED, actual);
        }
        [Fact]
        [Order(5)]
        public void commentaire_Deletecommentaire_null()
        {
            // Arrange
            const int ID = 1;

            // Act
            Manager.Delete(ID);
            Commentaire unCommentaireModif = Manager.Get(ID);
            Commentaire actual = unCommentaireModif;

            // Assert
            Assert.Null(actual);
        }
    }
}
