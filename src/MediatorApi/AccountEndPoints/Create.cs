using Ardalis.ApiEndpoints;
using AutoMapper;
using Mediator.Core.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Api.AccountEndPoints
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateAccountRequest>
        .WithActionResult<CreateAccountResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Create(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("account/create")]
        public override async Task<ActionResult<CreateAccountResponse>> HandleAsync(CreateAccountRequest request, CancellationToken cancellationToken = default)
        {
            var createCommand = _mapper.Map<CreateAccountCommand>(request);

            var account = await _mediator.Send(createCommand, cancellationToken);

            return Ok(_mapper.Map<CreateAccountResponse>(account));
        }
    }
}
