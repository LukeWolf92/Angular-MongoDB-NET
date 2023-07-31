using Microsoft.AspNetCore.Mvc;
using RestfulApi.Entities;
using RestfulApi.Services;

namespace RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class InvoiceServiceController : ControllerBase
{
    private readonly IInvoice _invoiceService;
    public InvoiceServiceController(IInvoice invoiceService) => _invoiceService = invoiceService;

    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_invoiceService.GetInvoices());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("[action]")]
    public ActionResult<string> GetAllByCustomer(string IDcustomer)
    {
        try
        {
            return Ok(_invoiceService.GetInvoices(IDcustomer));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("[action]")]
    public IActionResult Insert([FromBody] Invoice customer)
    {
        try
        {
            _invoiceService.SaveInvoice(customer);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("[action]")]
    public IActionResult Update(Invoice customer)
    {
        try
        {
            _invoiceService.SaveInvoice(customer);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("[action]")]
    public IActionResult Delete(string id)
    {
        try
        {
            return Ok(_invoiceService.DeleteInvoice(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}