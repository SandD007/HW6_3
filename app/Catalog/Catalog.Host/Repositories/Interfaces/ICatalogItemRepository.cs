using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<string> Delete(int id);
    Task<CatalogItem?> GetItemById(int id);
    Task<List<CatalogItem>> GetItemByBrand(int id);
    Task<List<CatalogItem>> GetItemByType(int id);
    Task<List<CatalogBrand>> GetItemsBrands();
    Task<List<CatalogType>> GetItemsTypes();
}