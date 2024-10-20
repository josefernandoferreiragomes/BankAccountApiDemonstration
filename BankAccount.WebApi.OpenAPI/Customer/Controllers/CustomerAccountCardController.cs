using BankAccount.WebAPI.DAL;
using BankAccount.WebApi.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics;

namespace BankAccount.WebApi.OpenAPI.Features.Customer.Controllers;

//[Authorize]
[Microsoft.AspNetCore.Mvc.Route("api/customeraccountcard")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class CustomerAccountCardController(
    ILogger<CustomerAccountCardController> logger,
    ICustomerService customerService) : ControllerBase 
{
    //private readonly ILogger<CustomerAccountCardController> _logger;
    //private readonly ICustomerService _customerService;

    [HttpGet, Microsoft.AspNetCore.Mvc.Route("list")]
    [ProducesResponseType<BankAccount.WebAPI.DAL.CustomerAccountCard>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public List<BankAccount.WebAPI.DAL.CustomerAccountCard> CustomerAccountCardList(int customerId)
    {
        using var _ = logger.BeginScope($"[CustomerId={customerId}");
        var response = customerService.ListCustomerAccountCardAsync(customerId)?.ToList();
        
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

