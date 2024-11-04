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
[ApiVersion("2.0")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerV2Controller(
    ILogger<CustomerV2Controller> logger,
    ICustomerService customerService) : ControllerBase 
{

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("get")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<BankAccount.WebAPI.DAL.Customer>, BadRequest<string>>> CustomerGetV2(int customerId)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.GetCustomerByIdAsync(customerId);

        return TypedResults.Ok(response);
    }

    /// <summary>
    /// Updates a customer.
    /// </summary>
    /// <param name="customer">The customer object.</param>
    [HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]
    [MapToApiVersion("2.0")]    
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BankAccount.WebAPI.DAL.Customer), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<BankAccount.WebAPI.DAL.Customer>, BadRequest<string>>> CustomerUpdateV2([FromBody] BankAccount.WebAPI.DAL.Customer customer)
    {
        using var _ = logger.BeginScope($"[CustomerId={customer.CustomerId}");
        var response = await customerService.UpdateCustomerAsync(customer);
        if (response == null)
        {
            return TypedResults.BadRequest("Unable to update customer.");
        }
        return TypedResults.Ok(response);
    }

}

