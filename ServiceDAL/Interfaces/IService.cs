using ServiceDAL.BusinessLayer;
using System;

namespace ServiceDAL.Interfaces
{
    public interface IService: IDisposable
    {
        RolesManager RolesManager { get; }
        VilleManager VilleManager { get; }
        CommentaireManager CommentaireManager { get; }
        TypeManager TypeManager { get; }
        CategorieManager CategorieManager { get; }
        AdresseManager AdresseManager { get; }
        PersonneManager PersonneManager { get; }


        RessourcesManager  RessourcesManager { get; }
        void Dispose();

    }
}
