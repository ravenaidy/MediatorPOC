using System;
using Mediator.Shared.AutoMapperExtentions.Contracts;

namespace Mediator.Api.AccountEndPoints
{
    public record GetAllAccountsResponse : IMapFrom<Core.Models.Account>
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}