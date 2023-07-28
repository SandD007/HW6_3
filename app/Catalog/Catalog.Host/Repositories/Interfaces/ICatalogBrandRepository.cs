using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Add(string name);
        Task<CatalogBrand?> Update(int id, string brand);
        Task<string> Delete(int id);
    }
}
