using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<string> Delete(int id);
    Task<CatalogItem?> GetItemById(int id);
    Task<List<CatalogItem>> GetItemsByBrand(int id);
    Task<List<CatalogItem>> GetItemsByType(int id);

    Task<List<CatalogBrand>> GetItemsBrands();

    Task<List<CatalogType>> GetItemsTypes();
}