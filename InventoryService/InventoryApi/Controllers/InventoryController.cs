using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using InventoryApi.Services;
using InventoryApi.Models;

namespace InventoryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly InventoryService productService;

    public InventoryController(InventoryService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductItem>>> GetAllProducts()
    {
        var products =  await productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProduct(Guid id)
    {
        var product = await productService.GetProductAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductItem>> PostProduct(ProductItem product)
    {
        var result = await productService.AddProductAsync(product);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<ProductItem>>> UpdateProduct(Guid id, ProductItem product)
    {
        var result = await productService.UpdateProductAsync(id, product);
        if (result is  null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductItem>> DeleteProduct(Guid id)
    {
        var result = await productService.DeleteProductAsync(id);
        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
