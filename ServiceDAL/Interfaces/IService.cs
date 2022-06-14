using ServiceDAL.BusinessLayer;
using System;

namespace ServiceDAL.Interfaces
{
    public interface IService: IDisposable
    {
        AdresseManager AdresseManager { get; }
        ArchiveManager ArchiveManager { get; }
        CategorieManager CategorieManager { get; }
        CommentaireManager CommentaireManager { get; }
        FavoriManager FavoriManager { get; }
        HistoriqueManager HistoriqueManager { get; }
        PersonneManager PersonneManager { get; }
        RessourcesManager RessourcesManager { get; }
        RolesManager RolesManager { get; }
        TypeManager TypeManager { get; }
        VilleManager VilleManager { get; }
        new void Dispose();

    }
}
