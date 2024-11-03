using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;
using Asp.Versioning;


namespace BankAccount.WebApi.OpenAPI.Features.Customer.Controllers;

//[Authorize]
[ApiController]
[ControllerName("Customer")]
[Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerV1Controller(
    ILogger<CustomerV1Controller> logger,
    ICustomerService customerService) : ControllerBase 
{
    //private readonly ILogger<CustomerController> _logger;
    //private readonly ICustomerService _customerService;

    [HttpPost, Microsoft.AspNetCore.Mvc.Route("create")]    
    [MapToApiVersion("1.0")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerCreateV1(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        using var _ = logger.BeginScope($"[FirstName={firstName}");
        var response = await customerService.CreateCustomerAsync(firstName, lastName, email, phoneNumber, dateOfBirth);
        
        return response;
    }   

    //API versioning demonstration
    [HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]   
    [MapToApiVersion("1.0")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerUpdateV1(int customerId, string firstName, string lastName, string email, string phoneNumber)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.UpdateCustomerAsync(customerId, firstName, lastName, email, phoneNumber);

        return response;
    }

    ////API versioning demonstration
    //[HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]
    //[ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    //[ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[MapToApiVersion("2.0")]
    //public async Task<BankAccount.WebAPI.DAL.Customer> CustomerUpdate(BankAccount.WebAPI.DAL.Customer customer)
    //{
    //    using var _ = logger.BeginScope($"[CustomerId={customer.CustomerId}");
    //    var response = await customerService.UpdateCustomerAsync(customer.CustomerId, customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber);

    //    return response;
    //}

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("get")]   
    [MapToApiVersion("1.0")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerGetV1(int customerId)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.GetCustomerByIdAsync(customerId);

        return response;
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("test")]   
    [MapToApiVersion("1.0")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public ActionResult<string> TestV1()
    {
        logger.LogInformation("CustomerControler method Test started...");
        Debug.WriteLine("CustomerControler method Test started...");
        return Ok("Service replies to request.");
    }
}

