using System.Collections.Generic;
using Mediator.Core.Caching.Contracts;
using MediatR;
using Microsoft.Extensions.Options;

namespace Mediator.Core.Account.Queries
{
    public record GetAllAccountsQuery : IRequest<List<Models.Account>>, ICacheable
    {
        public string CacheKey { get; set; } = "Accounts|GetAllAccounts";
    }
}