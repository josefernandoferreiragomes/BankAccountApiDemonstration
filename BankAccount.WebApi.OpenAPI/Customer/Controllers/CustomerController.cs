using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankAccount.WebApi.OpenAPI.Features.Customer.Controllers;

//[Authorize]
[Microsoft.AspNetCore.Mvc.Route("api/customer")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerController(
    ILogger<CustomerController> logger,
    ICustomerService customerService) : ControllerBase 
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    [HttpPost, Microsoft.AspNetCore.Mvc.Route("create")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerCreate(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        using var _ = _logger.BeginScope($"[FirstName={firstName}");
        var response = await _customerService.CreateCustomerAsync(firstName, lastName, email, phoneNumber, dateOfBirth);
        
        return response;
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("test")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public ActionResult<string> Test()
    {
        return Ok("Service replies to request.");
    }
}

