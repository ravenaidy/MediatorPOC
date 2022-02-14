using System.Threading.Tasks;

namespace Mediator.Core.Account.Contracts
{
    public interface IAccountRepository
    {
        Task<bool> UserNameExists(string userName);
        Task<Models.Account> GetAccount(string userName, string password);        
        Task<Models.Account> AddAccount(Models.Account account);
    }
}
