using Moq;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Data;
using Catalog.Host.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using FluentAssertions;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        // arrange
        var testResult = new CatalogItem
        {
            Id = _testItem.Id,
            Name = _testItem.Name,
            Description = _testItem.Description,
            Price = _testItem.Price,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogBrandId = _testItem.CatalogBrandId,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrand = _testItem.CatalogBrand,
            CatalogType = _testItem.CatalogType,
        };

        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        // arrange
        CatalogItem? testResult = null;
        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Success()
    {
        // arrange
        var testResult = "item has be deleted";

        _catalogItemRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Delete(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        // arrange
        var testResult = "not found this item";

        _catalogItemRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Delete(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetItemById_Success()
    {
        // arrange
        var testResult = new CatalogItem
        {
            Id = _testItem.Id,
            Name = _testItem.Name,
            Description = _testItem.Description,
            Price = _testItem.Price,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogBrandId = _testItem.CatalogBrandId,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrand = _testItem.CatalogBrand,
            CatalogType = _testItem.CatalogType,
        };

        _catalogItemRepository.Setup(s => s.GetItemById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetItemById(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetItemById_Failed()
    {
        // arrange
        CatalogItem? testResult = null;

        _catalogItemRepository.Setup(s => s.GetItemById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetItemById(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetItemsBrands_Success()
    {
        // arrange
        var testList = new List<CatalogBrand>
        {
        new CatalogBrand { Id = 1, Brand = "TestBrand" }
        };

        _catalogItemRepository.Setup(s => s.GetItemsBrands()).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsBrands();

        // assert
        Assert.Equal(testList, result);
    }

    [Fact]
    public async Task GetItemsBrands_Failed()
    {
        // arrange
        var testList = new List<CatalogBrand>();

        _catalogItemRepository.Setup(s => s.GetItemsBrands()).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsBrands();

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetItemsTypes_Success()
    {
        // arrange
        var testList = new List<CatalogType>
        {
        new CatalogType { Id = 1, Type = "TestType" }
        };

        _catalogItemRepository.Setup(s => s.GetItemsTypes()).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsTypes();

        // assert
        Assert.Equal(testList, result);
    }

    [Fact]
    public async Task GetItemsTypes_Failed()
    {
        // arrange
        var testList = new List<CatalogType>();

        _catalogItemRepository.Setup(s => s.GetItemsTypes()).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsTypes();

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetItemsByBrand_Success()
    {
        // arrange
        var testList = new List<CatalogItem>
        {
        new CatalogItem
        {
            Id = _testItem.Id,
            Name = _testItem.Name,
            Description = _testItem.Description,
            Price = _testItem.Price,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogBrandId = _testItem.CatalogBrandId,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrand = _testItem.CatalogBrand,
            CatalogType = _testItem.CatalogType,
        }
        };

        _catalogItemRepository.Setup(s => s.GetItemByBrand(
            It.IsAny<int>())).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsByBrand(_testItem.CatalogBrandId);

        // assert
        Assert.Equal(testList, result);
    }

    [Fact]
    public async Task GetItemsByBrand_Failed()
    {
        // arrange
        var testList = new List<CatalogItem>();

        _catalogItemRepository.Setup(s => s.GetItemByBrand(
            It.IsAny<int>())).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsByBrand(_testItem.CatalogBrandId);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetItemsByType_Success()
    {
        // arrange
        var testList = new List<CatalogItem>
        {
        new CatalogItem
        {
            Id = _testItem.Id,
            Name = _testItem.Name,
            Description = _testItem.Description,
            Price = _testItem.Price,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogBrandId = _testItem.CatalogBrandId,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrand = _testItem.CatalogBrand,
            CatalogType = _testItem.CatalogType,
        }
        };

        _catalogItemRepository.Setup(s => s.GetItemByType(
            It.IsAny<int>())).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsByType(_testItem.CatalogTypeId);

        // assert
        Assert.Equal(testList, result);
    }

    [Fact]
    public async Task GetItemsByType_Failed()
    {
        // arrange
        var testList = new List<CatalogItem>();

        _catalogItemRepository.Setup(s => s.GetItemByType(
            It.IsAny<int>())).ReturnsAsync(testList);

        // act
        var result = await _catalogService.GetItemsByType(_testItem.CatalogTypeId);

        // assert
        Assert.Empty(result);
    }
}