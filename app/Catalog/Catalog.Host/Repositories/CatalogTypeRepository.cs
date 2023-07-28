using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Type = name,
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<CatalogType?> Update(int id, string name)
        {
            var item = await _dbContext.CatalogTypes.
            FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                item.Type = name;
            }

            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<string> Delete(int id)
        {
            var item = await _dbContext.CatalogTypes.
            FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                _dbContext.CatalogTypes.Remove(item);
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
