using ServiceDAL.BusinessLayer;
using System;

namespace ServiceDAL.Interfaces
{
    public interface IService: IDisposable
    {
        VilleManager VilleManager { get; }
        AdresseManager AdresseManager { get; }
        PersonneManager PersonneManager { get; }
        void Dispose();
    }
}
