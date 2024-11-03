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
[ApiVersion("2.0")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerV2Controller(
    ILogger<CustomerV2Controller> logger,
    ICustomerService customerService) : ControllerBase 
{

    //API versioning demonstration
    [HttpPut, Microsoft.AspNetCore.Mvc.Route("update")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.OK)]
    [ProducesResponseType<BankAccount.WebAPI.DAL.Customer>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<BankAccount.WebAPI.DAL.Customer> CustomerUpdateV2(int customerId, string firstName, string lastName, string email, string phoneNumber)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = await customerService.UpdateCustomerAsync(customerId, firstName, lastName, email, phoneNumber);

        return response;
    }

}

