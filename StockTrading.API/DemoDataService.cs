using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StockTrading.BusinessLogic;
using StockTrading.DataAccess.Models;

namespace StockTrading.API
{
    public class DemoDataService: IHostedService
    {
        private readonly ICompanyService _companyService;
        private readonly IOrderService _orderService;

        public DemoDataService(ICompanyService companyService, IOrderService orderService)
        {
            _companyService = companyService;
            _orderService = orderService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Set up companies
            _companyService.AddCompany(new Company {Symbol = "AMZN"});
            _companyService.AddCompany(new Company { Symbol = "TSLA"});
            _companyService.AddCompany(new Company { Symbol = "BLKFNCH" });
            
            // Set up company initial shares
            //_companyService.IssueShares("AMZN", 100000, 40);
            //_companyService.IssueShares("TSLA", 10000, 200);
            //_companyService.IssueShares("BLKFNCH", 600, 60);

            //// Set up starting orders
            //_orderService.AddOrder(new Order("AMZN", 40, 40, 100, OrderType.Buy));
            //_orderService.AddOrder(new Order("TSLA", 200, 200, 2000, OrderType.Buy));
            //_orderService.AddOrder(new Order("BLKFNCH", 60, 60, 300, OrderType.Buy));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // No need to do anything
            return Task.CompletedTask;
        }
    }
}
