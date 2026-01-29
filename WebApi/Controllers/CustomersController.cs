using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Entity;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CodeMagDbContext _db;

    public CustomersController(CodeMagDbContext db)
    {
        _db = db;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest req)
    {
        var exists = await _db.Customers.AnyAsync(x => x.GLN == req.GLN);
        if (exists)
            return Conflict("Customer with same GLN already exists.");

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            GLN = req.GLN,
            Description = req.Description
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { customer.Id }, customer);
    }

    // READ (LIST)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Customers
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.GLN,
                x.Description
            })
            .ToListAsync();

        return Ok(list);
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerRequest req)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();

        var glnUsed = await _db.Customers
            .AnyAsync(x => x.GLN == req.GLN && x.Id != id);

        if (glnUsed)
            return Conflict("GLN already used by another customer.");

        customer.Name = req.Name;
        customer.GLN = req.GLN;
        customer.Description = req.Description;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _db.Customers
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
            return NotFound();

        if (customer.Products.Any())
            return Conflict("Customer has products. Delete products first.");

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
