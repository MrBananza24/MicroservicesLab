using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    readonly ProductContext context;
    public ProductController(ProductContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(Guid id)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) 
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        product.Id = Guid.Empty;
        var result = await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return Ok(result);
    }
}
