using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.Add(request.Name);
        return Ok(new AddTypeResponse<int?>() { Id = result });
    }

    [HttpPut]
    [ProducesResponseType(typeof(GetItemByIdResponse<CatalogBrand>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(CreateUpdateTypeRequest request)
    {
        var result = await _catalogTypeService.Update(request.Id, request.Name);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(GetItemByIdRequest request)
    {
        var result = await _catalogTypeService.Delete(request.Id);
        return Ok(result);
    }
}