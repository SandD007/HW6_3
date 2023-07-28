using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<string> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(id));
    }

    public Task<CatalogItem?> GetItemById(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemById(id));
    }

    public Task<List<CatalogBrand>> GetItemsBrands()
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemsBrands());
    }

    public Task<List<CatalogItem>> GetItemsByBrand(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemByBrand(id));
    }

    public Task<List<CatalogItem>> GetItemsByType(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemByType(id));
    }

    public Task<List<CatalogType>> GetItemsTypes()
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemsTypes());
    }
}