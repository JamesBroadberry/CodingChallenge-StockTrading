using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockTrading.BusinessLogic;
using StockTrading.API.Model;
using StockTrading.DataAccess.Models;

namespace StockTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return _companyService.GetCompanies();
        }

        [HttpPost("{companySymbol}/shares")]
        public Order IssueShare(string companySymbol, [FromBody] IssueSharesRequest request)
        {
            var sharesIssuedOrder = _companyService.IssueShares(companySymbol, request.Quantity, request.Price);
            if (sharesIssuedOrder == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            return sharesIssuedOrder;
        }

    }
}
