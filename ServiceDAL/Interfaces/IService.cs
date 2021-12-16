using ServiceDAL.BusinessLayer;
using System;

namespace ServiceDAL.Interfaces
{
    public interface IService: IDisposable
    {
        RolesManager RolesManager { get; }
        VilleManager VilleManager { get; }
        TypeManager TypeManager { get; }
        CategorieManager CategorieManager { get; }
        AdresseManager AdresseManager { get; }
        PersonneManager PersonneManager { get; }
        RessourcesManager RessourcesManager { get; }
        new void Dispose();
    }
}
