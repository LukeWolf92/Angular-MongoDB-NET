using Microsoft.AspNetCore.Mvc;
using RestfulApi.Entities;
using RestfulApi.Services;

namespace RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CustomerServiceController : ControllerBase
{
    private readonly ICustomer _customerService;
    public CustomerServiceController(ICustomer customerService) => _customerService = customerService;

    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_customerService.GetCustomers());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("[action]")]
    public IActionResult Get(string ID)
    {
        try
        {
            return Ok(_customerService.GetCustomer(ID));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("[action]")]
    public IActionResult Insert([FromBody] Customer customer)
    {
        try
        {
            _customerService.SaveCustomer(customer);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("[action]")]
    public IActionResult Update([FromBody] Customer customer)
    {
        try
        {
            _customerService.SaveCustomer(customer);
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
            return Ok(_customerService.DeleteCustomer(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
