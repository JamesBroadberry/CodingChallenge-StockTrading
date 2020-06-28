using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockTrading.BusinessLogic;
using StockTrading.API.Model;
using StockTrading.DataAccess.Models;

namespace StockTrading.API.Controllers
{
    /// <summary>
    /// The controller for all company related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// The controller for all company related operations
        /// </summary>
        /// <param name="companyService">The company service to use when fulfilling API requests</param>
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>A list of all of the companies</returns>
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return _companyService.GetCompanies();
        }

        /// <summary>
        /// Issue shares for a company 
        /// </summary>
        /// <param name="companySymbol">The company to issue shares for</param>
        /// <param name="request">The payload containing how many shares and at what price to issue</param>
        /// <returns>The sell order which was created by issuing shares</returns>
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
