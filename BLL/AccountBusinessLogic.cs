using DALBank.DAL;
using DALBank.Model;

namespace DALBank.BLL
{
    public class AccountBusinessLogic
    {
        public AccountRepository Repo { get; set; }
        public AccountBusinessLogic(AccountRepository repo)
        {
            Repo = repo;
        }

        public Account GetAccountById(int id)
        {
            return Repo.Get(a => a.Id == id);
            // a => return a.Id == id;
        }

        public List<Account> GetAllAccounts()
        {
            return Repo.GetList(a => true).ToList();
        }

        public double GetBalance(int id)
        {
            var account = GetAccountById(id);

            if(account == null)
            {
                throw new Exception("Account not found");
            } else if (!account.IsActive)
            {
                throw new Exception("Account is not active");
            }
            
            return account.Balance;
        }

        public double GetTotalBalanceOfCustomer(int customerId)
        {
            return GetAllAccounts().Where(a => a.CustomerId == customerId).Sum(a => a.Balance);
        }
        public void Withdraw(int id, double amount)
        {
            // get the account
            Account account = GetAccountById(id);

            if(account != null)
            {
               if(amount <= 0)
                {
                    throw new Exception("Amount to withdraw must be greater than $0");
                } else if (amount > account.Balance)
                {
                    throw new Exception("Insufficient funds to withdraw given amount.");
                } else if (!account.IsActive)
                {
                    throw new Exception("Account is inactive.");
                } else
                {
                    account.Balance -= amount;
                    Repo.Save();
                }
            } else
            {
                throw new Exception("Account not found");
            }
        }

        public List<Account> GetAllAccountsOfCustomer(int customerId)
        {
            return Repo.GetList(a => a.CustomerId == customerId && a.IsActive).ToList();
        }
        public void CloseAccount(Account account)
        {
            account.IsActive = false;
        }
    }
}
