using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextTech.Core.MediatR.Commands;
using NextTech.Core.MediatR.Queries;
using NextTech.PayHub.Accounting.Application;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AccountingController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public AccountingController(
          IMediator mediator,
           ICommandBus commandBus,
          IQueryBus queryBus,
          ILogger<AccountingController> logger
          )
        {
            this.mediator = mediator;
            this.commandBus = commandBus;
            this.queryBus = queryBus;
            this.logger = logger;
        }

        [HttpPost("authorize-card")]
        public async Task<IActionResult> AuthorizeCreditCard([FromBody] AuthorizeCreditCardRequest query)
        {
            var authorizeCardQuery = AuthorizeCardQuery.Create(query.CardOwner, query.CardNumber, query.CardExpireDate, query.CvcNumber);

            var result = await queryBus.Send<AuthorizeCardQuery, AuthorizeCardQueryResponse>(authorizeCardQuery);

            return Ok(result);
        }
    }
}
