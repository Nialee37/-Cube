using ServiceDAL.BusinessLayer;
using System;

namespace ServiceDAL.Interfaces
{
    public interface IService: IDisposable
    {
        RolesManager RolesManager { get; }
        VilleManager VilleManager { get; }
        AdresseManager AdresseManager { get; }
        PersonneManager PersonneManager { get; }
        new void Dispose();
    }
}
