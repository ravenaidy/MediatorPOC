using Mediator.Api.DTO;
using Mediator.Shared.AutoMapperExtentions.Contracts;

namespace Mediator.Api.AccountEndPoints
{
    public record CreateAccountResponse : ApiResponse, IMapFrom<Core.Models.Account>
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
    }
}
