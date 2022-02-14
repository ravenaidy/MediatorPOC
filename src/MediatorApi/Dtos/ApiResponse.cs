using System.Net;

namespace Mediator.Api.DTO
{
    public record ApiResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string ErrorMessage { get; init; }
    }
}
