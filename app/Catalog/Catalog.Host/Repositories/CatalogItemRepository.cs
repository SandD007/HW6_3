using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            AvailableStock = availableStock,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems.
        Include(i => i.CatalogBrand).
        Include(i => i.CatalogType).
        FirstOrDefaultAsync(i => i.Id == id);

        if (item != null)
        {
            item.CatalogBrandId = catalogBrandId;
            item.CatalogTypeId = catalogTypeId;
            item.Description = description;
            item.Name = name;
            item.AvailableStock = availableStock;
            item.PictureFileName = pictureFileName;
            item.Price = price;
        }

        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.CatalogItems.
        Include(i => i.CatalogBrand).
        Include(i => i.CatalogType).
        FirstOrDefaultAsync(i => i.Id == id);

        return result;
    }

    public async Task<string> Delete(int id)
    {
        var item = await _dbContext.CatalogItems.
        Include(i => i.CatalogBrand).
        Include(i => i.CatalogType).
        FirstOrDefaultAsync(i => i.Id == id);

        if (item != null)
        {
            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            return "item has be deleted";
        }
        else
        {
            await _dbContext.SaveChangesAsync();
            return "not found this item";
        }
    }

    public async Task<CatalogItem?> GetItemById(int id)
    {
        var item = await _dbContext.CatalogItems.
            Include(i => i.CatalogBrand).
            Include(i => i.CatalogType).
            FirstOrDefaultAsync(i => i.Id == id);

        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<CatalogItem>> GetItemByBrand(int id)
    {
        var item = await _dbContext.CatalogItems.
            Include(i => i.CatalogType).
            Include(i => i.CatalogBrand).
            Where(i => i.CatalogBrandId == id).
            ToListAsync();

        await _dbContext.SaveChangesAsync();
        return item.ToList();
    }

    public async Task<List<CatalogItem>> GetItemByType(int id)
    {
        var item = await _dbContext.CatalogItems.
        Include(i => i.CatalogType).
        Include(i => i.CatalogBrand).
        Where(i => i.CatalogTypeId == id).
        ToListAsync();

        await _dbContext.SaveChangesAsync();
        return item.ToList();
    }

    public async Task<List<CatalogBrand>> GetItemsBrands()
    {
        var item = await _dbContext.CatalogBrands.
        ToListAsync();

        await _dbContext.SaveChangesAsync();
        return item.ToList();
    }

    public async Task<List<CatalogType>> GetItemsTypes()
    {
        var item = await _dbContext.CatalogTypes.
        ToListAsync();

        await _dbContext.SaveChangesAsync();
        return item.ToList();
    }
}