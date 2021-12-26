using MediatR;
using Microsoft.AspNetCore.Mvc;
using Offeror.LinkedInApi.Domain.Contracts;

namespace Offeror.LinkedInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator _mediator)
        {
            _mediator = _mediator;
        }

        [HttpPost]
        public async Task<ActionResult<IResult<AccountDTO>>> ParseItem([FromBody] CreateAccountCommand url) =>
            Ok(await _provider.FillComponent(url));
    }
}
