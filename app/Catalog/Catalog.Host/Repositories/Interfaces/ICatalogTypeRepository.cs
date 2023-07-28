using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(string name);
        Task<CatalogType?> Update(int id, string name);
        Task<string> Delete(int id);
    }
}
