using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Mediator.Core.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mediator.Api.AccountEndPoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<GetAllAccountsResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAll(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpPost("account/getall")]
        public override async Task<ActionResult<List<GetAllAccountsResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllAccountsQuery(), cancellationToken);
            if (result == null) 
                return NoContent();

            return Ok(_mapper.Map<List<GetAllAccountsResponse>>(result));
        }
    }
}