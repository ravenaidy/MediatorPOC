using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mediator.Core.Account.Contracts
{
    public interface IAccountRepository
    {
        Task<bool> UserNameExists(string userName);
        Task<Models.Account> GetAccount(Models.Account account);
        Task<List<Models.Account>> GetAllAccount();
        Task AddAccount(Models.Account account);
    }
}
