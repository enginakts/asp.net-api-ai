using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAi.Project01_ApiDemp.Context;
using NetCoreAi.Project01_ApiDemp.Entities;

namespace NetCoreAi.Project01_ApiDemp;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ApiContext _context;

    public CustomerController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> CustomerList()
    {
        var value = await _context.Customers.ToListAsync();
        if (value == null)
            return NotFound();

        return Ok(value);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetCustomer(int? id)
    {
        if (id == null)
            return NotFound("id alanı boş olamaz");

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

        if (customer == null)
            return NotFound("Girdiğiniz id ye sahip kullanıcı bulunamadı");

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        if (customer == null)
            return NotFound();

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return Ok("Müşteri başarı ile eklendi");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(int? id)
    {
        if (id == null)
            return NotFound("id alanınına değer girmediniz");

        var value = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

        if (value == null)
            return NotFound("Girdiğiniz id ye sahip kullanıcı bulunamadı");

        _context.Customers.Remove(value);
        await _context.SaveChangesAsync();

        return Ok("Kullanıcı başarılı bir şekilde silindi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(Customer model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Gerekli alanları doğru giriniz");

        var customer = await _context.Customers.FirstOrDefaultAsync(c =>
            c.CustomerId == model.CustomerId
        );

        if (customer == null)
            return NotFound("Kullanıcı bulunamadı");

        customer.CustomerName = model.CustomerName;
        customer.CustomerSurName = model.CustomerSurName;
        customer.CustomerBalance = model.CustomerBalance;

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return Ok("Kullanıcı başarıyla güncellendi");
    }
}
