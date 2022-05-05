using DALBank.DAL;
using DALBank.Model;

namespace DALBank.BLL
{
    public class CustomerBusinessLogic
    {
        public CustomerRepository Repo { get; set; }
        public CustomerBusinessLogic(CustomerRepository repo)
        {
            Repo = repo;
        }

        public void CloseAllCustomerAccounts(int id, AccountBusinessLogic accountBL)
        {
            Customer customer = Repo.GetCustomerById(id);

            if(customer == null)
            {
                throw new Exception("Customer does not exist");
            }

            List<Account> customerAccounts = accountBL.GetAllAccountsOfCustomer(customer.Id);

            foreach(Account account in customerAccounts)
            {
                accountBL.CloseAccount(account);
            }

            Repo.Save();
        }

        public double GetTotalAccountBalance(int id, AccountBusinessLogic abl)
        {
            Customer customer = Repo.GetCustomerById(id);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            else
            {
                return abl.GetAllAccountsOfCustomer(id).Sum(a => a.Balance);
            } 

        }
    }
}
