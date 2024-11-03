using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;


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

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("list")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IEnumerable<BankAccount.WebAPI.DAL.Customer>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<BankAccount.WebAPI.DAL.Customer>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<IEnumerable<BankAccount.WebAPI.DAL.Customer>>, BadRequest<string>>> CustomerListV1()
    {
        using var _ = logger.BeginScope("list all customers");
        var response = await customerService.GetAllCustomersAsync();

        return TypedResults.Ok(response);
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("get")]
    [MapToApiVersion("1.0")]    
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<BankAccount.WebAPI.DAL.Customer>, BadRequest<string>>> CustomerGetV1(int customerId)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.GetCustomerByIdAsync(customerId);     

        return TypedResults.Ok(response);
    }

    [HttpPost, Microsoft.AspNetCore.Mvc.Route("create")]    
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<BankAccount.WebAPI.DAL.Customer>, BadRequest<string>>> CustomerCreateV1(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        using var _ = logger.BeginScope($"[FirstName={firstName}");
        var response = await customerService.CreateCustomerAsync(firstName, lastName, email, phoneNumber, dateOfBirth);
        
        return TypedResults.Ok(response);
    }   

    //API versioning demonstration
    [HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]   
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<BankAccount.WebAPI.DAL.Customer>, BadRequest<string>>> CustomerUpdateV1(int customerId, string firstName, string lastName, string email, string phoneNumber)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.UpdateCustomerAsync(customerId, firstName, lastName, email, phoneNumber);

        return TypedResults.Ok(response);
    }       

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("test")]   
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public Results<Ok<string>, BadRequest<string>> TestV1()
    {
        logger.LogInformation("CustomerControler method Test started...");
        Debug.WriteLine("CustomerControler method Test started...");

        return TypedResults.Ok("Service replies to request.");
    }
}

