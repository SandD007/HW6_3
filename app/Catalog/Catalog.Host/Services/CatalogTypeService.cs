using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public Task<int?> Add(string name)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Add(name));
        }

        public Task<string> Delete(int id)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Delete(id));
        }

        public Task<CatalogType?> Update(int id, string name)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Update(id, name));
        }
    }
}
