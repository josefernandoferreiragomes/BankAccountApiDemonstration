using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using Asp.Versioning;

namespace BankAccount.WebApi.OpenAPI.Features.Customer.Controllers;

//[Authorize]
[ApiController]
[ControllerName("CustomerAccountCard")]
[Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerAccountCardV1Controller(
    ILogger<CustomerAccountCardV1Controller> logger,
    ICustomerService customerService) : ControllerBase 
{
    //private readonly ILogger<CustomerAccountCardController> _logger;
    //private readonly ICustomerService _customerService;

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("list")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.CustomerAccountCard>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public List<BankAccount.WebAPI.DAL.CustomerAccountCard> CustomerAccountCardList(int customerId)
    {
        List<BankAccount.WebAPI.DAL.CustomerAccountCard> response = new List<CustomerAccountCard>();
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        response = customerService.ListCustomerAccountCardAsync(customerId)?.ToList() ?? response;
        
        return response;
    }

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("test")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public ActionResult<string> Test()
    {
        logger.LogInformation("CustomerAccountCardControler method Test started...");
        Debug.WriteLine("CustomerAccountCardControler method Test started...");
        return Ok("Service replies to request.");
    }
}

