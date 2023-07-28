using System.Xml.Linq;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = name,
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<CatalogBrand?> Update(int id, string brand)
        {
            var item = await _dbContext.CatalogBrands.
            FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                item.Brand = brand;
            }

            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<string> Delete(int id)
        {
            var item = await _dbContext.CatalogBrands.
            FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                _dbContext.CatalogBrands.Remove(item);
                await _dbContext.SaveChangesAsync();
                return "item has be deleted";
            }
            else
            {
                await _dbContext.SaveChangesAsync();
                return "not found this item";
            }
        }
    }
}
