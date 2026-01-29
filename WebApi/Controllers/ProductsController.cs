using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Entity;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
public class ProductsController : ControllerBase
{
    private readonly CodeMagDbContext _db;

    public ProductsController(CodeMagDbContext db)
    {
        _db = db;
    }

    // CREATE PRODUCT FOR CUSTOMER
    [HttpPost("customers/{customerId:guid}/products")]
    public async Task<IActionResult> Create(Guid customerId, CreateProductRequest req)
    {
        var customer = await _db.Customers.FindAsync(customerId);
        if (customer == null)
            return NotFound("Customer not found.");

        var exists = await _db.Products
            .AnyAsync(x => x.CustomerId == customerId && x.GTIN == req.GTIN);

        if (exists)
            return Conflict("Product with same GTIN already exists for this customer.");


        if (req.GTIN.Length != 14 || !req.GTIN.All(char.IsDigit))
            return BadRequest("GTIN must be 14 digits.");


        var product = new Product
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            GTIN = req.GTIN,
            Name = req.Name,
            Description = req.Description,
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            product.Id,
            product.CustomerId,
            product.GTIN,
            product.Name,
            product.Description
        });
    }

    // LIST PRODUCTS FOR CUSTOMER
    [HttpGet("customers/{customerId:guid}/products")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var products = await _db.Products
            .Where(x => x.CustomerId == customerId)
            .Select(x => new
            {
                x.Id,
                x.GTIN,
                x.Name,
                x.Description,
            })
            .ToListAsync();

        return Ok(products);
    }

    // UPDATE PRODUCT
    [HttpPut("products/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest req)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        var exists = await _db.Products
            .AnyAsync(x => x.CustomerId == product.CustomerId
                        && x.GTIN == req.GTIN
                        && x.Id != id);

        if (exists)
            return Conflict("GTIN already used for this customer.");

        if (req.GTIN.Length != 14 || !req.GTIN.All(char.IsDigit))
            return BadRequest("GTIN must be 14 digits.");

        product.GTIN = req.GTIN;
        product.Name = req.Name;
        product.Description = req.Description;

        await _db.SaveChangesAsync();
        return Ok();
    }

    // DELETE PRODUCT
    [HttpDelete("products/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _db.Products
            .Include(x => x.WorkOrders)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        if (product.WorkOrders.Any())
            return Conflict("Product has work orders. Delete them first.");

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
