using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(string name);
        Task<CatalogBrand?> Update(int id, string brand);
        Task<string> Delete(int id);
    }
}
