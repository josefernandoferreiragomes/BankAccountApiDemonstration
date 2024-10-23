using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;

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
    //private readonly ILogger<CustomerController> _logger;
    //private readonly ICustomerService _customerService;

    [HttpPost, Microsoft.AspNetCore.Mvc.Route("create")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerCreate(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        using var _ = logger.BeginScope($"[FirstName={firstName}");
        var response = await customerService.CreateCustomerAsync(firstName, lastName, email, phoneNumber, dateOfBirth);
        
        return response;
    }

    //[HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]
    //[ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    //[ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //public async Task<BankAccount.WebAPI.DAL.Customer> CustomerUpdate(BankAccount.WebAPI.DAL.Customer customer)
    //{
    //    TODO parallel version of the update, refactored
    //    return response;
    //}
    [HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerUpdate(BankAccount.WebAPI.DAL.Customer customer)
    {
        using var _ = logger.BeginScope($"[CustomerId={customer.CustomerId}");
        var response = await customerService.UpdateCustomerAsync(customer.CustomerId, customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber);

        return response;
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("get")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerGet(int customerId)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.GetCustomerByIdAsync(customerId);

        return response;
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("test")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public ActionResult<string> Test()
    {
        logger.LogInformation("CustomerControler method Test started...");
        Debug.WriteLine("CustomerControler method Test started...");
        return Ok("Service replies to request.");
    }
}

